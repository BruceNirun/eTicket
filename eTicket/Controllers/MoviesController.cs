using eTicket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbcontext _context;

        public MoviesController(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allMovie = await _context.Movies.ToListAsync();
            return View(allMovie);
        }
    }
}
