using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnMathRu_0._1.Controllers
{
    public class HomeController : Controller
    {
        private readonly LavandaDB _context;

        public HomeController(LavandaDB context)
        {
            _context = context;
        }

        // GET: OrderInfoes1
        public async Task<IActionResult> Index()
        {
            return View();
        }

        //public IActionResult siteRules()
        //{
        //    return View("siteRules");
        //}

        public IActionResult OrderView()
        {
            return View();
        }
    }
}
