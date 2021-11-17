using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChartJSCore.Helpers;
using ChartJSCore.Models;
using FinancePlanner.Database;
using FinancePlanner.Models;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            StatisticsViewModel statisticsViewModel = new StatisticsViewModel()
            {
                Plans = await _context.Plans.Where(plan => plan.UserId == userId).ToListAsync(),
                Events = await _context.Events.Where(x => x.UserId == userId).ToListAsync(),
                EventCategories = await _context.EventCategories.ToListAsync(),
                EventStatuses = await _context.EventStatuses.ToListAsync(),
                PlanStatuses = await _context.PlanStatuses.ToListAsync(),
                GoalTypes = await _context.GoalTypes.ToListAsync()
            };
            List<int> planIds = new List<int>();
            List<int> categoryIds = new List<int>();
            statisticsViewModel.EventCategoryMap = new Dictionary<string, double?>();
            statisticsViewModel.SpendingByDateMap = new Dictionary<string, double?>();
            statisticsViewModel.SpendingByCategoryMap = new Dictionary<string, double?>();
            
            foreach (var _event in statisticsViewModel.Events)
            {
                if (_event.EventCategoryId != 0)
                {
                    var category = await _context.EventCategories.FindAsync(_event.EventCategoryId);
                    var category_name = category.CategoryTitle;
                        
                    if (statisticsViewModel.EventCategoryMap.ContainsKey(category_name))
                    {
                        statisticsViewModel.EventCategoryMap[category_name]++;    
                    }
                    else
                    {
                        statisticsViewModel.EventCategoryMap.Add(category_name, 1);
                    }

                    if (_event.GoalTypeId != 0)
                    {
                        var type = await _context.GoalTypes.FindAsync( _event.GoalTypeId);
                    
                        var type_name = type.Name;    
                        
                        if (type_name == "Financial")
                        {
                            var eventDate = _event.StartDate.Date.ToString();
                            var financialEvent = await _context.FinancialEvents.FindAsync( _event.FinancialEventId);
                            double? amount = financialEvent.Amount;
                            
                            if (statisticsViewModel.SpendingByDateMap.ContainsKey(eventDate))
                            {
                                statisticsViewModel.SpendingByDateMap[eventDate] += amount;    
                            }
                            else
                            {
                                statisticsViewModel.SpendingByDateMap.Add(eventDate, amount);
                            }
                            
                            if (statisticsViewModel.SpendingByCategoryMap.ContainsKey(category_name))
                            {
                                statisticsViewModel.SpendingByCategoryMap[category_name] += amount;    
                            }
                            else
                            {
                                statisticsViewModel.SpendingByCategoryMap.Add(category_name, amount);
                            }
                            
                        }
                    }
                }
            }
            
            foreach (var plan in statisticsViewModel.Plans)
            {
                planIds.Add(plan.Id);
                categoryIds.Add(plan.EventCategoryId);
            }
            
            statisticsViewModel.Goals = await _context.Goals.Where(g => planIds.Contains(g.PlanId)).ToListAsync();
            statisticsViewModel.EventCategories = await _context.EventCategories.Where(g => categoryIds.Contains(g.Id)).ToListAsync();

            var numericalTargets = new List<double?>();
            var numericalProgresses = new List<double?>();
            foreach (var goal in statisticsViewModel.Goals)
            {
                numericalTargets.Add(goal.NumericalTarget);
                numericalProgresses.Add(goal.NumericalProgress);
            }
            
            
            statisticsViewModel.FinancialEvents = new List<FinancialEvent>();
            
            foreach (var _event in statisticsViewModel.Events)
            {
                if (_event.FinancialEventId != 0)
                {
                    var financialEvent = await _context.FinancialEvents.FindAsync( _event.FinancialEventId);
                    statisticsViewModel.FinancialEvents.Add(financialEvent);
                }
            }
            
            var eventCategories = new SelectList(_context.EventCategories.ToList(),"Id", "CategoryTitle");
            ViewBag.EventCategoryList = eventCategories;
            
            ViewBag.NumericalTargetList = numericalTargets;
            ViewBag.NumericalProgressList = numericalProgresses;
            
            var inProgressCount = 0;
            var fitnessCount = 0;
            var financialCount = 0;

            var inProgressStatusId = statisticsViewModel.PlanStatuses.Find(x => x.Status == "In Progress").Id;
            var categoriesMap = new Dictionary<int, int>();
            
            var fitnessTypeId = statisticsViewModel.GoalTypes.Find(x => x.Name == "Fitness").Id;
            var financialTypeId = statisticsViewModel.GoalTypes.Find(x => x.Name == "Financial").Id;


            foreach (var plan in statisticsViewModel.Plans)
            {
                if (plan.PlanStatusId == inProgressStatusId)
                {
                    inProgressCount++;
                }
            }
            
            foreach (var _event in statisticsViewModel.Events)
            {
                if (categoriesMap.ContainsKey(_event.EventCategoryId))
                {
                    categoriesMap[_event.EventCategoryId]++;
                }
                else
                {
                    categoriesMap.Add(_event.EventCategoryId, 1);
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
            var mostFrequentCategory = await _context.EventCategories.FindAsync(mostFrequentCategoryId);
            var mostFrequentCategoryName = mostFrequentCategory.CategoryTitle;
         
            
            ViewData["In_progress"] = inProgressCount.ToString();
            ViewData["Most_used_category"] = mostFrequentCategoryName;
            ViewData["Fitness"] = fitnessCount.ToString();
            ViewData["Financial"] = financialCount.ToString();

            

            ViewData["Title"] = "Statistics";
            ViewData["spending_date_chart"] = GetSpendingByDateChart(statisticsViewModel);
            ViewData["spending_category_chart"] = GetSpendingByCategoryChart(statisticsViewModel);
            // ViewData["goal_chart"] = GetGoalChart(statisticsViewModel);
            ViewData["event_category_chart"] = GetEventCategoryChart(statisticsViewModel);
            return View(statisticsViewModel);
        }
        
        private Chart GetEventCategoryChart(StatisticsViewModel statisticsViewModel)
        {
            Chart chart = new Chart();
            Data data = new Data();
            data.Datasets = new List<Dataset>();
            chart.Data = data;
            chart.Type = Enums.ChartType.Pie;
            
            var categoryTitles = statisticsViewModel.EventCategoryMap.Keys.ToList();
            var categoryValues = statisticsViewModel.EventCategoryMap.Values.ToList();

            data.Labels = categoryTitles;
            PieDataset dataset = new PieDataset()
            {
                Label = "TestChart",
                Data = categoryValues,
                BackgroundColor = GetColors(statisticsViewModel.EventCategoryMap.Keys.Count)
            };
            data.Datasets.Add(dataset);
            return chart;
        }
        
        private Chart GetSpendingByCategoryChart(StatisticsViewModel statisticsViewModel)
        {
            Chart chart = new Chart();
            Data data = new Data();
            data.Datasets = new List<Dataset>();
            chart.Data = data;
            chart.Type = Enums.ChartType.PolarArea;

            var categories = statisticsViewModel.SpendingByCategoryMap.Keys.ToList();
            var values = statisticsViewModel.SpendingByCategoryMap.Values.ToList();

            data.Labels = categories;
            PolarDataset dataset = new PolarDataset()
            {
                Label = "TestChart",
                Data = values,
                BackgroundColor = GetColors(statisticsViewModel.SpendingByCategoryMap.Keys.Count)
            };
            data.Datasets.Add(dataset);
            return chart;
        }


        private Chart GetSpendingByDateChart(StatisticsViewModel statisticsViewModel)
        {
            Chart chart = new Chart();
            Data data = new Data();
            data.Datasets = new List<Dataset>();
            chart.Data = data;
            chart.Type = Enums.ChartType.Line;

            var dates = statisticsViewModel.SpendingByDateMap.Keys.ToList();
            var values = statisticsViewModel.SpendingByDateMap.Values.ToList();

            data.Labels = dates;
            LineDataset dataset = new LineDataset()
            {
                Label = "Spending",
                Data = values,
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
            data.Datasets.Add(dataset);
            return chart;
        }
        
        private Chart GetGoalChart(StatisticsViewModel statisticsViewModel)
        {
            Chart chart = new Chart();
            Data data = new Data();
            data.Datasets = new List<Dataset>();
            chart.Data = data;
            chart.Type = Enums.ChartType.Line;
            

            data.Labels = new List<string>() { "asd", "asd", "asd", "asd", "asd", "asd", "asd" };
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
            data.Datasets.Add(dataset);
            return chart;
        }


        private List<ChartColor> GetColors(int count)
        {
            var colors = new List<ChartColor>();

            for (int i = 0; i < count; i++)
            {
                Random rnd = new Random();
                byte red  = (byte) rnd.Next(0, 256);
                byte green  = (byte) rnd.Next(0, 256);
                byte blue  = (byte) rnd.Next(0, 256);
                ChartColor color = new ChartColor();
                
                color.Red = red;
                color.Blue = green;
                color.Green = blue;
                color.Alpha = 0.5;
                
                colors.Add(color);
            }

            return colors;
        }
    }
}