using Microsoft.AspNetCore.Mvc;
using StudentPortalWeb.Models;
using StudentPortalWeb.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace StudentPortalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
         private readonly StudentPortalContext _context;


        public HomeController(
            ILogger<HomeController> logger,
            StudentPortalContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var students = _context.Students
            .OrderBy(s => s.FullName)
            .ToList();
            Console.WriteLine($"Students found: {students.Count}");

            return View(students);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
