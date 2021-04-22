using Microsoft.AspNetCore.Mvc;

namespace FinancePlanner.Controllers
{
    public class StatisticsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}