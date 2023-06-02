using eTicket.Data;
using eTicket.Data.Services;
using eTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allCinema = await _service.GetAllAsync();
            return View(allCinema);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }

                return View(cinema);
            }
            _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetail = await _service.GetByIdAsync(id);

            if (cinemaDetail == null)
            {
                return View("NotFound");
            }
            return View(cinemaDetail);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetail = await _service.GetByIdAsync(id);

            if (cinemaDetail == null)
            {
                return View("NotFound");
            }
            return View(cinemaDetail);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id , Logo , Name , Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }

                return View(cinema);
            }
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetail = await _service.GetByIdAsync(id);

            if (cinemaDetail == null)
            {
                return View("NotFound");
            }
            return View(cinemaDetail);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaDetail = await _service.GetByIdAsync(id);

            if (cinemaDetail == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
