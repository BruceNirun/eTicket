using Microsoft.AspNetCore.Mvc;

namespace eTicket.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
