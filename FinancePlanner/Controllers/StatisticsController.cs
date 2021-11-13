using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChartJSCore.Helpers;
using ChartJSCore.Models;
using FinancePlanner.Database;
using FinancePlanner.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;

namespace FinancePlanner.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ILogger<StatisticsController> _logger;
        private readonly PlannerContext _context;

        public StatisticsController(ILogger<StatisticsController> logger, PlannerContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            Chart chart = new Chart();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            StatisticsViewModel newStatistic = new StatisticsViewModel()
            {
                Plans = await _context.Plans.Where(plan => plan.UserId == userId).ToListAsync()
            };
            List<int> planIds = new List<int>();
            List<int> categoryIds = new List<int>();
            
            foreach (var plan in newStatistic.Plans)
            {
                planIds.Add(plan.Id);
                categoryIds.Add(plan.EventCategoryId);
            }
            
            newStatistic.Goals = await _context.Goals.Where(g => planIds.Contains(g.PlanId)).ToListAsync();
            newStatistic.EventCategories = await _context.EventCategories.Where(g => categoryIds.Contains(g.Id)).ToListAsync();

            var numericalTargets = new List<double?>();
            var numericalProgresses = new List<double?>();
            foreach (var goal in newStatistic.Goals)
            {
                numericalTargets.Add(goal.NumericalTarget);
                numericalProgresses.Add(goal.NumericalProgress);
            }
            
            
            var eventCategories = new SelectList(_context.EventCategories.ToList(),"Id", "CategoryTitle");
            ViewBag.EventCategoryList = eventCategories;
            
            ViewBag.NumericalTargetList = numericalTargets;
            ViewBag.NumericalProgressList = numericalProgresses;




            chart.Type = Enums.ChartType.Line;
            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();

            data.Labels = new List<string>() { "January", "February", "March", "April", "May", "June", "July" };
            LineDataset dataset = new LineDataset()
            {
                Label = "TestChart",
                Data = new List<double?>{ 65, 59, 80, 81, 56, 55, 40 },
                Fill = "false",
                LineTension = 0.1,
                BackgroundColor = ChartColor.FromRgba(75, 192, 192, 0.4),
                BorderColor = ChartColor.FromRgb(75, 192, 192),
                BorderCapStyle = "butt",
                BorderDash = new List<int> { },
                BorderDashOffset = 0.0,
                BorderJoinStyle = "miter",
                PointBorderColor = new List<ChartColor> {ChartColor.FromRgb(75, 192, 192)},
                PointBackgroundColor = new List<ChartColor> {ChartColor.FromHexString("#ffffff")},
                PointBorderWidth = new List<int> {1},
                PointHoverRadius = new List<int> {5},
                PointHoverBackgroundColor = new List<ChartColor> {ChartColor.FromRgb(75, 192, 192)},
                PointHoverBorderColor = new List<ChartColor> {ChartColor.FromRgb(220, 220, 220)},
                PointHoverBorderWidth = new List<int> {2},
                PointRadius = new List<int> {1},
                PointHitRadius = new List<int> {10},
                SpanGaps = false
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            ViewData["chart"] = chart;
            return View(newStatistic);
        }
    }
}