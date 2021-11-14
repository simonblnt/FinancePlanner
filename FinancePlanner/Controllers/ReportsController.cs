using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinancePlanner.Database;
using FinancePlanner.Models;
using FinancePlanner.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinancePlanner.Controllers
{
    public class ReportsController : Controller
    {
        private readonly PlannerContext _context;

        public ReportsController(PlannerContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException("User.FindFirstValue(ClaimTypes.NameIdentifier)");
            var _reportViewModel = new ReportViewModel
            {
                Reports = await _context.Reports.Where(x => x.UserId == userId).ToListAsync(),
                Plans = await _context.Plans.Where(x => x.UserId == userId).ToListAsync()
            };

            return View(_reportViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException("User.FindFirstValue(ClaimTypes.NameIdentifier)");
            
            var eventList = await _context.Events.Where(x => x.UserId == userId).ToListAsync();
            var planList = await _context.Plans.Where(x => x.UserId == userId).ToListAsync();
            
            var eventSelectList = new SelectList(eventList,"Id", "Title");
            var planSelectList = new SelectList(planList,"Id", "Title");

            var sendingSchedules = new List<string> {"hourly", "daily", "weekly"};

            ViewBag.EventList = eventSelectList;
            ViewBag.PlanList = planSelectList;
            ViewBag.SendingSchedules = sendingSchedules;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Report newReport)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException("User.FindFirstValue(ClaimTypes.NameIdentifier)");
                newReport.UserId = userId;

                // if (newReport.SendingSchedule == "hourly")
                // {
                //     newReport.NextScheduledDate = DateTime.Now.AddHours(1);
                // } else if (newReport.SendingSchedule == "daily")
                // {
                //     newReport.NextScheduledDate = DateTime.Now.AddDays(1);
                // } else if (newReport.SendingSchedule == "weekly")
                // {
                //     newReport.NextScheduledDate = DateTime.Now.AddDays(7);
                // } else
                // {
                //     newReport.NextScheduledDate = DateTime.Now;
                // }
                
                // For testing purposes, override schedule on report creation
                newReport.ScheduleOverride = true;

                await _context.AddAsync(newReport);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newReport);
        }
    }
}