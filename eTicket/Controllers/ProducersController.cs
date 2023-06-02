using eTicket.Data;
using eTicket.Data.Services;
using eTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;

        public ProducersController(IProducersService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allProducer = await _service.GetAllAsync();
            return View(allProducer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }

                return View(producer);
            }
            _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var producerDetail = await _service.GetByIdAsync(id);

            if (producerDetail == null)
            {
                return View("NotFound");
            }
            return View(producerDetail);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetail = await _service.GetByIdAsync(id);

            if (producerDetail == null)
            {
                return View("NotFound");
            }
            return View(producerDetail);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id , ProfilePictureURL , FullName , Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }

                return View(producer);
            }
            await _service.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetail = await _service.GetByIdAsync(id);

            if (producerDetail == null)
            {
                return View("NotFound");
            }
            return View(producerDetail);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetail = await _service.GetByIdAsync(id);

            if (producerDetail == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
