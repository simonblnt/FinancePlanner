using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FinancePlanner.Database;
using FinancePlanner.Models.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FinancePlanner.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly PlannerContext _context;

        public LoginController(PlannerContext context)
        {
            _context = context;
        }
        

        public IActionResult Index()
        {
            return View();
        }


        [Route("login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (CheckLogin(username, password))
            {
                ViewBag.error = "Success";
                return View("Index");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Index");    
            }
        }



        private bool CheckLogin(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (user != null)
            {
                return true;    
            }
            else
            {
                return false;
            }


        }

    }
}