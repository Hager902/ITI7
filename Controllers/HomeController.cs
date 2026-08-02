using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentPortalWeb.Models;
using StudentPortalWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace StudentPortalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentPortalContext _context;
        private readonly IHagerStampService _stampA;
        private readonly IHagerStampService _stampB;

        public HomeController(
            ILogger<HomeController> logger,
            StudentPortalContext context,
            IHagerStampService stampA,
            IHagerStampService stampB
            )
        {
            _logger = logger;
            _context = context;
            _stampA = stampA;
            _stampB = stampB;
        }
    public async Task<IActionResult> Index(){
    var students = await _context.Students
        .OrderBy(s => s.FullName)
        .ToListAsync();

    ViewBag.Owner = _stampA.Owner;
    ViewBag.StampA = _stampA.Stamp;
    ViewBag.StampB = _stampB.Stamp;

    return View(students);
}
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}