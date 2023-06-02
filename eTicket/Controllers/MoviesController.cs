using eTicket.Data;
using eTicket.Data.ViewModels;
using eTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var allMovie = await _context.Movies
                .Include(s => s.Cinema)
                .OrderBy(s => s.StartDate)
                .ToListAsync();
            return View(allMovie);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            // สร้างรายการของ Actors
            var actors = _context.Actors.ToList();
            ViewBag.Actors = new SelectList(actors, "Id", "FullName");

            // สร้างรายการของ Producers
            var producers = _context.Producers.ToList();
            if (producers == null || producers.Count() == 0)
            {
                // Handle this scenario: No producers found or database operation failed
            }
            ViewBag.Producers = new SelectList(producers, "Id", "FullName");

            // สร้างรายการของ Cinemas
            var cinemas = _context.Cinemas.ToList();
            ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");

            return View();
        }
        [HttpPost]
        public IActionResult Create(MovieVm data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };
             _context.Movies.AddAsync(newMovie);
             _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
