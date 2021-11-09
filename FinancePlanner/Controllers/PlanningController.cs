using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinancePlanner.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinancePlanner.Models;
using FinancePlanner.Models.Main;
using FinancePlanner.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinancePlanner.Controllers
{
    public class PlanningController : Controller
    {
        private readonly ILogger<PlanningController> _logger;
        private readonly PlannerContext _context;

        public PlanningController(ILogger<PlanningController> logger, PlannerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException("User.FindFirstValue(ClaimTypes.NameIdentifier)");

            PlanningViewModel planningViewModel = new PlanningViewModel
            {
                Plans = await _context.Plans.Where(x => x.UserId == userId).ToListAsync(),
            };

            List<int> planIds = new List<int>();
            List<int> categoryIds = new List<int>();
            foreach (var plan in planningViewModel.Plans)
            {
                planIds.Add(plan.Id);
                categoryIds.Add(plan.EventCategoryId);
            }

            planningViewModel.Goals = await _context.Goals.Where(g => planIds.Contains(g.PlanId)).ToListAsync();
            planningViewModel.EventCategories = await _context.EventCategories.Where(g => categoryIds.Contains(g.Id)).ToListAsync();
            
            var eventCategories = new SelectList(_context.EventCategories.ToList(),"Id", "CategoryTitle");
            ViewBag.EventCategoryList = eventCategories;
            
            return View(planningViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        { 
            var eventCategories = new SelectList(_context.EventCategories.ToList(),"Id", "CategoryTitle");

            ViewBag.EventCategoryList = eventCategories;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanningViewModel newPlanModel)
        {
            
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException("User.FindFirstValue(ClaimTypes.NameIdentifier)");
                var newPlan = new Plan
                {
                    Title = newPlanModel.Title,
                    EventCategoryId = newPlanModel.EventCategoryId,
                    UserId = userId
                };
                await _context.AddAsync(newPlan);
                await _context.SaveChangesAsync();

                foreach (var goal in newPlanModel.Goals)
                {
                    var newGoal = new Goal
                    {
                        PlanId = newPlan.Id,
                        Title = goal.Title,
                        NumericalTarget = goal.NumericalTarget,
                        NumericalProgress = goal.NumericalProgress
                    };
                    await _context.AddAsync(newGoal);
                }
                
                
                
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newPlanModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}