using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinancePlanner.Database;
using FinancePlanner.Models;
using FinancePlanner.Models.Main;
using FinancePlanner.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancePlanner.Controllers
{
    public class EventsController : Controller
    {
        private readonly ILogger<EventsController> _logger;
        private readonly PlannerContext _context;

        public EventsController(ILogger<EventsController> logger, PlannerContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            EventViewModel eventViewModel = new EventViewModel
            {
                Events = await _context.Events.Where(x => x.UserId == userId).ToListAsync(),
                EventCategories = await _context.EventCategories.ToListAsync(),
                EventStatuses = await _context.EventStatuses.ToListAsync(),
                GoalTypes = await _context.GoalTypes.ToListAsync(),
            };

            var inProgressCount = 0;
            var fitnessCount = 0;
            var financialCount = 0;

            var inProgressStatusId = eventViewModel.EventStatuses.Find(x => x.Status == "In Progress").Id;
            var categoriesMap = new Dictionary<int, int>();
            
            var fitnessTypeId = eventViewModel.GoalTypes.Find(x => x.Name == "Fitness").Id;
            var financialTypeId = eventViewModel.GoalTypes.Find(x => x.Name == "Financial").Id;
                
            foreach (var _event in eventViewModel.Events)
            {
                if (categoriesMap.ContainsKey(_event.EventCategoryId))
                {
                    categoriesMap[_event.EventCategoryId]++;
                }
                else
                {
                    categoriesMap.Add(_event.EventCategoryId, 1);
                }
                
                if (_event.EventStatusId == inProgressStatusId)
                {
                    inProgressCount++;
                }
                
                if (_event.GoalTypeId == fitnessTypeId)
                {
                    fitnessCount++;
                } else if (_event.GoalTypeId == financialTypeId)
                {
                    financialCount++;
                }
            }
                
            var mostFrequentCategoryId = categoriesMap.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            var mostFrequentCategoryName = eventViewModel.EventCategories.Find(x => x.Id == mostFrequentCategoryId)?.CategoryTitle;
         
            ViewData["Title"] = "Events";
            ViewData["In_progress"] = inProgressCount.ToString();
            ViewData["Most_used_category"] = mostFrequentCategoryName;
            ViewData["Fitness"] = fitnessCount.ToString();
            ViewData["Finance"] = financialCount.ToString();
            return View(eventViewModel);
        }
        
        public async Task<IActionResult> Create()
        { 
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException("User.FindFirstValue(ClaimTypes.NameIdentifier)");
            
            
            List<Plan> planList = await _context.Plans.Where(x => x.UserId == userId).ToListAsync();
            var categoryList = await _context.EventCategories.ToListAsync();
            var goalTypeList = await _context.GoalTypes.ToListAsync();
            var eventStatusList = await _context.EventStatuses.ToListAsync();

            List<Goal> goalList = new List<Goal>();
            foreach (var plan in planList)
            {
                var goals = await _context.Goals.Where(x => x.PlanId == plan.Id).ToListAsync();
                goalList.AddRange(goals);
            }
            
            var eventCategoriesSelectList = new SelectList(categoryList,"Id", "CategoryTitle");
            var goalTypesSelectList = new SelectList(goalTypeList,"Id", "Name");
            var goalsSelectList = new SelectList(goalList,"Id", "Title");
            var eventStatusSelectList = new SelectList(eventStatusList,"Id", "Status");

            ViewBag.EventStatusList = eventStatusSelectList;
            ViewBag.EventCategoryList = eventCategoriesSelectList;
            ViewBag.GoalTypeList = goalTypesSelectList;
            ViewBag.GoalList = goalsSelectList;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,StartDate,EndDate,EventCategoryId,GoalId,GoalTypeId,EventStatusId")] Event newEvent, int financialAmount, int fitnessDistance, int fitnessDuration)
        {
            newEvent.CreatedAt = DateTime.Now;
            newEvent.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                bool goalAdded = false;
                var goalType = await _context.GoalTypes.FindAsync(newEvent.GoalTypeId);
                var goalStatuses = await _context.GoalStatuses.ToListAsync();
                
                
                if (newEvent.GoalId != 0)
                {
                    goalAdded = true;
                }

                if (goalType.Name == "Financial")
                {
                    var financialEvent = new FinancialEvent {Amount = financialAmount};

                    if (goalAdded)
                    {
                        var goal = await _context.Goals.FindAsync(newEvent.GoalId);
                        goal.NumericalProgress += financialAmount;
                        if (goal.NumericalProgress >= goal.NumericalTarget)
                        {
                            goal.GoalStatusId = goalStatuses.Find(x => x.Status == "Succeeded").Id;
                        }
                        else
                        {
                            goal.GoalStatusId = goalStatuses.Find(x => x.Status == "In Progress").Id;
                        }

                        _context.Update(goal);
                    }

                    await _context.FinancialEvents.AddAsync(financialEvent);
                    await _context.SaveChangesAsync();
                    newEvent.FinancialEventId = financialEvent.Id;
                } 
                else if (goalType.Name == "Fitness")
                {
                    var fitnessEvent = new FitnessEvent();
                    fitnessEvent.Distance = fitnessDistance;
                    fitnessEvent.Duration = fitnessDuration;
                    
                    if (goalAdded)
                    {
                        var goal = await _context.Goals.FindAsync(newEvent.GoalId);
                        goal.NumericalProgress += fitnessDistance;
                        if (goal.NumericalProgress >= goal.NumericalTarget)
                        {
                            goal.GoalStatusId = goalStatuses.Find(x => x.Status == "Succeeded").Id;
                        }
                        else
                        {
                            goal.GoalStatusId = goalStatuses.Find(x => x.Status == "In Progress").Id;
                        }
                        
                        _context.Update(goal);
                    }
                    
                    await _context.FitnessEvents.AddAsync(fitnessEvent);
                    await _context.SaveChangesAsync();
                    
                    newEvent.FitnessEventId = fitnessEvent.Id;
                }
                else if (goalType.Name == "General")
                {
                    if (goalAdded)
                    {
                        var goal = await _context.Goals.FindAsync(newEvent.GoalId);
                        goal.GoalStatusId = goalStatuses.Find(x => x.Status == "Succeeded").Id;
                        
                        _context.Update(goal);
                    }
                    await _context.SaveChangesAsync();
                }

                await _context.AddAsync(newEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newEvent);
        }

        [HttpGet]
        public async Task<IActionResult> CreateEventCategory()
        {
            var categoryList = await _context.EventCategories.ToListAsync();
            var eventCategories = new SelectList(categoryList, "Id", "CategoryTitle");

            ViewBag.EventCategoryList = eventCategories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventCategory([Bind("CategoryTitle, IsDayLong")] EventCategory newEventCategory)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(newEventCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newEventCategory);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event _event = await _context.Events.FindAsync(id);
            if (_event == null)
            {
                return NotFound();
            }
            
            var eventCategories = new SelectList(_context.EventCategories.ToList(),"Id", "CategoryTitle");

            ViewBag.EventCategoryList = eventCategories;
            return View(_event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Title,Description,StartDate,EndDate,EventCategoryId")] Event _event, int financialAmount, int fitnessDistance, int fitnessDuration)
        {
            if (id != _event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(_event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(_event);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _event = await _context.Events.FirstOrDefaultAsync(item => item.Id == id);
            if (_event == null)
            {
                return NotFound();
            }

            return View(_event);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eEvent = await _context.Events.FindAsync(id);
            
            if (eEvent.FinancialEventId != 0)
            {
                var financialEvent = await _context.FinancialEvents.FindAsync(eEvent.FinancialEventId);
                _context.FinancialEvents.Remove(financialEvent);
            }
            
            if (eEvent.FitnessEventId != 0)
            {
                var fitnessEvent = await _context.FitnessEvents.FindAsync(eEvent.FitnessEventId);
                _context.FitnessEvents.Remove(fitnessEvent);
            }

            _context.Events.Remove(eEvent);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private bool EventExists(object id)
        {
            throw new NotImplementedException();
        }
    }
}