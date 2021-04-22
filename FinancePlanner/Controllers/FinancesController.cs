using Microsoft.AspNetCore.Mvc;

namespace FinancePlanner.Controllers
{
    public class FinancesController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}