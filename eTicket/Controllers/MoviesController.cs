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
        //[HttpPost]
        //public IActionResult Create(MovieVm data, List<int> actorIds)
        //{
        //    var newMovie = new Movie()
        //    {
        //        Name = data.Name,
        //        Description = data.Description,
        //        Price = data.Price,
        //        ImageURL = data.ImageURL,
        //        CinemaId = data.CinemaId,
        //        StartDate = data.StartDate,
        //        EndDate = data.EndDate,
        //        MovieCategory = data.MovieCategory,
        //        ProducerId = data.ProducerId
        //    };

        //    _context.Movies.AddAsync(newMovie);
        //    _context.SaveChangesAsync();

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<IActionResult> Create(MovieVm data, List<int> ActorId)
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

            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            foreach (var actorId in ActorId)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };

                _context.Actors_Movies.Add(newActorMovie);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }










        //public async Task<IActionResult> Details(int id)
        //{
        //    var result = await _context.Movies
        //        .Include(s => s.Cinema)
        //        .Include(s => s.Actor_Movie).ThenInclude(s => s.Actor)
        //        .Include(s => s.Producer)
        //        .FirstOrDefaultAsync(s => s.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }

        //    กรุ๊ปและเลือกข้อมูลนักแสดงเพียงครั้งแรกเท่านั้น
        //   var distinctActors = result.Actor_Movie.GroupBy(am => am.ActorId)
        //                                           .Select(g => g.First().Actor);

        //    result.Actor_Movie = distinctActors.Select(a => new Actor_Movie
        //    {
        //        ActorId = a.Id,
        //        Actor = a,
        //        MovieId = result.Id,
        //        Movie = result
        //    }).ToList();

        //    return View(result);
        //}

        //public async Task<IActionResult> Details(int id)
        //{
        //    var movieDetails = await _context.Movies
        //        .Include(c => c.Cinema)
        //        .Include(p => p.Producer)
        //        .Include(am => am.Actor_Movie).ThenInclude(a => a.Actor)
        //        .FirstOrDefaultAsync(n => n.Id == id);

        //    return View(movieDetails);
        //}
        //public async Task<IActionResult> Details(int id)
        //{
        //    var movieDetails = await _context.Movies
        //        .Include(c => c.Cinema)
        //        .Include(p => p.Producer)
        //        .Include(am => am.Actor_Movies).ThenInclude(am => am.Actor)
        //        .FirstOrDefaultAsync(n => n.Id == id);

        //    return View(movieDetails);
        //}

        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _context.Movies
                .Include(m => m.Actor_Movies).ThenInclude(am => am.Actor)
                .Include(m => m.Cinema)
                .Include(m => m.Producer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movieDetails == null)
            {
                return NotFound();
            }

            return View(movieDetails);
        }
    }
}
