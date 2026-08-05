using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortalWeb.Data;
using StudentPortalWeb.Models;

//Because create GET just returns an empty form no database query needed
//Because Edit is for changing an existing row, so we need to show the current values prefilled
//An empty form would lose all existing data the whole point is to load and display the real row
//Model would fail to match id to the parameter because names must match
//Because the in memory object is a C# reference, disconnected from the database so, reloading ensures we display what actually got saved to the database



 namespace StudentPortalWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentPortalContext _context;
 
        public StudentsController(StudentPortalContext context)
        {
            _context = context;
        }

        public IActionResult Create(){
            return View(new Student());
           
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student){
            if(!ModelState.IsValid){
                return View(student);
            }
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            TempData["Message"] =$"Student {student.FullName} created successfully.";
            return RedirectToAction(nameof(Index));
            }

            public IActionResult Hager(int id){
                return RedirectToAction(nameof(Hager), new { id = id });
            }


        public async Task<IActionResult> Details(int id){
            var student = await _context.Students.FindAsync(id);
            if(student == null){
                return NotFound();
            }
            return View(student);
        }
        [HttpGet]

        public async Task<IActionResult> Edit(int id){
            var student = await _context.Students.FindAsync(id);
            if(student == null){
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Student student){
            if(!id.Equals(student.Id)){
                return BadRequest();
            }
            else if(!ModelState.IsValid){
                return View(student);
            }
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            TempData["Message"] = $"Student {student.FullName} updated successfully.";
            return RedirectToAction(nameof(Hager), new { id = student.Id });
        }

        public async Task<IActionResult> HagerConfirmed(int id){
            var student = await _context.Students.FindAsync(id);
            if(student == null){
                return NotFound();
            }
           return View("Details", student);
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


