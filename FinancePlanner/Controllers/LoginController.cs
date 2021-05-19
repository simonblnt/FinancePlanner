using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FinancePlanner.Database;
using FinancePlanner.Models.Main;
using Microsoft.AspNetCore.Mvc;

namespace FinancePlanner.Controllers
{
    public class LoginController : Controller
    {
        private readonly PlannerContext _context;

        public LoginController(PlannerContext context)
        {
            
        }
    }
}