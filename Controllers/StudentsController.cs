using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortalWeb.Data;
using StudentPortalWeb.Models;


 namespace StudentPortalWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentPortalContext _context;
 
        public StudentsController(StudentPortalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int id){
            var student = await _context.Students.FindAsync(id);
            if(student == null){
                return NotFound();
            }
            return View(student);
        }

        public async Task<IActionResult> ByYear(int year){
            if (year < 1 || year > 4){
                return BadRequest("Year must be between 1 and 4.");
            }
            var students = await _context.Students
            .Where(s => s.YearOfStudy == year)
            .ToListAsync();

            ViewData["Year"] = year;
            return View("Index", students);
        }

        public async Task<IActionResult> Honors(string band)
        {
            var minGpa = 2.5;
            var students = band switch
            {
            "A" => await _context.Students.Where(s => s.Gpa >= 3.5)
            .ToListAsync(),
            "B" => await _context.Students.Where(s => s.Gpa >= 3.0 && s.Gpa < 3.5).
            ToListAsync(),
            "C" => await _context.Students.Where(s => s.Gpa >= 2.5 && s.Gpa < 3.0)
            .ToListAsync(),
            _ => new List<Student>()
        };

        ViewData["Band"] = band;
        return View("Index", students);
        }


        public async Task<IActionResult> Index(){
            var students = await _context.Students.ToListAsync();
            return View(students);
        }

           public async Task<IActionResult> Top(int count){
            var students = await _context.Students
            .OrderByDescending(s => s.Gpa)
            .Take(count)
            .ToListAsync();

            return View("Index", students);
           }

           public async Task<IActionResult> Intake(string code){
            var students = await _context.Students
            .Where(s => s.IntakeCode.Equals(code, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();

            return View("Index", students);
           }

           [Route("about/hager")]
           public async Task<IActionResult> About([FromQuery] double minGpa = 2.5){
            var students = await _context.Students
            .Where(s => s.Gpa >= minGpa)
            .OrderByDescending(s => s.FullName)
            .ToListAsync();

             return View("Index", students);
           }

    }
}


//Accepted because Max Year = 4 is valid because range is from (1,4) 
//MinGpa is optional, variable, and a filtering criterion rather than a resource identifier
//The top action is never invoked, No database query executes, No model binding occurs
//Similarity : Both implement IRouteConstraint and are called during route matching
//Difference : The int constraint validates type, IntakeCodeConstraint validates domain business logic
//Guarantee : The Route]attribute completely overrides conventional routing for that action