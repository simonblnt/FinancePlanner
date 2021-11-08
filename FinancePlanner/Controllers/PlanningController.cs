using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FinancePlanner.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinancePlanner.Models;
using FinancePlanner.Models.Main;
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

        public IActionResult Index()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<List<Plan>> GetPlans()
        {
            List<Plan> plans = await _context.Plans.ToListAsync();
            return plans;
        }
        private async Task<List<Goal>> GetGoals()
        {
            List<Goal> goals = await _context.Goals.ToListAsync();
            return goals;
        }
        private async Task<List<GoalType>> GetGoalTypes()
        {
            List<GoalType> goalTypes = await _context.GoalTypes.ToListAsync();
            return goalTypes;
        }
    }
}