using System;
using FinancePlanner.Database;
using FinancePlanner.Models.Main;
using Microsoft.AspNetCore.Mvc;

namespace FinancePlanner.Controllers
{
    public class SignUpController : Controller
    {
        private PlannerContext _context;
        
        public SignUpController(PlannerContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        [Route("signup")]
        public IActionResult SignUp(string firstName, string lastName, string userName, string password)
        {
            if (TrySignUp(firstName, lastName, userName, password))
            {
                ViewBag.error = "Success";
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                ViewBag.error = "Successn't";
                return View("Index");
            }
        }


        [HttpPost]
        private bool TrySignUp(string firstName, string lastName, string userName, string password)
        {
            var newUser = new User
            {
                FirstName = firstName, LastName = lastName, UserName = userName, Password = password, RegisteredAt = DateTime.Now
            };

            _context.Users.Add(newUser);
            
            return _context.SaveChanges() == 1;
        }
    }
}