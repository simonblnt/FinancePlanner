using System.Collections.Generic;
using System.Threading.Tasks;
using FinancePlanner.Database;
using FinancePlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancePlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly PlannerContext _context;

        public EventsController(PlannerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _context.Events.ToListAsync();
            return events;
        }
    }
}