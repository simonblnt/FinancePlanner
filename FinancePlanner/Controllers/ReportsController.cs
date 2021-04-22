using Microsoft.AspNetCore.Mvc;

namespace FinancePlanner.Controllers
{
    public class ReportsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}