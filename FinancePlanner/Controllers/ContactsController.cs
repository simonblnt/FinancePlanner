using Microsoft.AspNetCore.Mvc;

namespace FinancePlanner.Controllers
{
    public class ContactsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}