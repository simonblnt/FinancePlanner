using System;
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
            };
            
            return View(eventViewModel);
        }
        
        public async Task<IActionResult> Create()
        { 
            var eventCategories = new SelectList(_context.EventCategories.ToList(),"Id", "CategoryTitle");

            ViewBag.EventCategoryList = eventCategories;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,StartDate,EndDate,EventCategoryId")] Event newEvent, int financialAmount, int fitnessDistance, int fitnessDuration)
        {
            newEvent.CreatedAt = DateTime.Now;
            newEvent.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                if (financialAmount != 0)
                {
                    var financialEvent = new FinancialEvent {Amount = financialAmount};
                    await _context.FinancialEvents.AddAsync(financialEvent);
                    await _context.SaveChangesAsync();
                    newEvent.FinancialEventId = financialEvent.Id;
                }

                if (fitnessDistance != 0 && fitnessDuration != 0)
                {
                    var fitnessEvent = new FitnessEvent();
                    fitnessEvent.Distance = fitnessDistance;
                    fitnessEvent.Duration = fitnessDuration;
                    await _context.FitnessEvents.AddAsync(fitnessEvent);
                    await _context.SaveChangesAsync();
                    
                    newEvent.FitnessEventId = fitnessEvent.Id;
                }
                await _context.AddAsync(newEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newEvent);
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
            
            // todo: change default value of financial and fitness event ids to null instead of 0
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