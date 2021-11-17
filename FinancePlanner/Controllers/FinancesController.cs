using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinancePlanner.Database;
using FinancePlanner.Models;
using FinancePlanner.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancePlanner.Controllers
{
    public class FinancesController : Controller
    {
        private readonly PlannerContext _context;

        public FinancesController(PlannerContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            FinanceViewModel financeViewModel = new FinanceViewModel()
            {
                Events = await _context.Events.AsQueryable().Where(x => x.UserId == userId).ToListAsync(),
                Wallets = await _context.Wallets.AsQueryable().Where(x => x.UserId == userId).ToListAsync(),
                EventCategories = await _context.EventCategories.ToListAsync()
            };

            financeViewModel.FinancialEvents = new List<FinancialEvent>();
            foreach (var _event in financeViewModel.Events)
            {
                if (_event.FinancialEventId != 0)
                {
                    var financialEvent = await _context.FinancialEvents.FindAsync(_event.FinancialEventId);
                    financeViewModel.FinancialEvents.Add(financialEvent);
                }
            }

            @ViewData["Title"] = "Finances";
            return View(financeViewModel);
        }
    }
}