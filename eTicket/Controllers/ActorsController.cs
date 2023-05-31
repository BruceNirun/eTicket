using eTicket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Controllers
{
    public class ActorsController : Controller
    {
        private readonly AppDbcontext _context;

        public ActorsController(AppDbcontext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allActors = await _context.Actors.ToListAsync();
            return View();
        }
    }
}
