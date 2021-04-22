using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FinancePlanner.Database;
using FinancePlanner.Models.Main;
using Microsoft.AspNetCore.Mvc;

namespace FinancePlanner.Controllers
{
    public class UserController : Controller
    {
        private readonly PlannerContext _context;

        public UserController(PlannerContext context)
        {
            _context = context;
            if (_context.Users.Count() == 0)
            {
                _context.Users.Add(new User { Email = "simon.blnt93@gmail.com", FirstName = "admin", LastName = "user", Password = "admin", UserName = "admin", RegisteredAt = DateTime.Now});
                _context.SaveChanges();
            }
        }

    }
}