using Microsoft.AspNetCore.Mvc;

namespace FinancePlanner.Controllers
{
    public class TrackerController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}