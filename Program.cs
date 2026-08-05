using Microsoft.EntityFrameworkCore;
using StudentPortalWeb.Data;
using StudentPortalWeb.Models;
using StudentPortalWeb.Constraints;
  
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudentPortalContext>(options => 
options.UseSqlite("Data Source=StudentPortal.db"));


builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("intakeCode", typeof(IntakeCodeConstraint));
});

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("honorBand", typeof(HonorBandConstraint));
});

var app = builder.Build();
using (var scope = app.Services.CreateScope()){
    var context = scope.ServiceProvider.GetRequiredService<StudentPortalContext>();
    context.Database.EnsureCreated();

     if (!context.Students.Any())
    {
        var students = new List<Student>{
            new Student { FullName = "Yara Adel", YearOfStudy = 2, Gpa = 3.5, Email = "yara@iti.edu" },
            new Student { FullName = "Omar Hesham", YearOfStudy = 1, Gpa = 2.8, Email = "omar@iti.edu" },
            new Student { FullName = "Nada Samir", YearOfStudy = 2, Gpa = 3.9, Email = "nada@iti.edu" },
            new Student { FullName = "Kareem Fouad", YearOfStudy = 1, Gpa = 3.2, Email = "kareem@iti.edu" }
        };


        var instructors = new List<Instructor>
        {
            new Instructor { FullName = "Hamdy", YearsOfExperience = 10 },
            new Instructor { FullName = "Mona Khalil", YearsOfExperience = 6 }
        };

         
        context.Students.AddRange(students);
        context.Instructors.AddRange(instructors);
        context.SaveChanges();

        var courses = new List<Course>{
    new Course
    {
        CourseName = "Web Development Using .NET",
        Credits = 4,
        InstructorId = instructors[0].Id
    },
    new Course
    {
        CourseName = "Database Fundamentals",
        Credits = 3,
        InstructorId = instructors[1].Id
    }
};

context.Courses.AddRange(courses);
context.SaveChanges();
        }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
 
app.UseHttpsRedirection();
app.UseStaticFiles();
 
app.UseRouting();
 
app.UseAuthorization();

app.MapControllerRoute(
    name : "roster",
    pattern : "roster",
    defaults : new {controller = "Students", action = "Index"}
);

app.MapControllerRoute(
    name : "top",
    pattern : "students/top/{count:int:range(1, 4)}",
    defaults : new{controller = "Students", action = "Top"}
);

app.MapControllerRoute(
    name : "intake",
    pattern : "students/intake/{code:intakeCode}",
    defaults : new {controller = "Students", action = "Intake"}
);

app.MapControllerRoute(
    name : "details",
    pattern : "students/details/{id:int}",
    defaults : new {controller = "Students", action = "Details"}
);

app.MapControllerRoute(
    name : "byYear",
    pattern : "students/byYear/{year:int:range(1, 4)}",
    defaults : new {controller = "Students", action = "ByYear"}
);

app.MapControllerRoute(
    name : "honors",
    pattern : "students/honors/{band:honorBand}",
    defaults : new {controller = "Students", action = "Honors"}
);
 
 app.MapControllerRoute(
    name : "default",
    pattern : "{controller=Home}/{action=Index}/{id:int?}"
    );

    app.Run();


    //Lab ID: 15 ,(15 mod 4) + 1 = 4  Max Year = 4 
    // 2.5 + (15 mod 3) × 0.5 = 2.5   Min Gpa = 2.5
    // 15 mod 3 = 0, iti + A = itiA   Intake Code = itiA
    

 