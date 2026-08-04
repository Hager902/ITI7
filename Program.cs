using Microsoft.EntityFrameworkCore;
using StudentPortalWeb.Data;
using StudentPortalWeb.Constraints;
  
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudentPortalContext>(options => 
options.UseSqlite("Data Source=StudentPortal.db"));


builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("intakeCode", typeof(IntakeCodeConstraint));
});

var app = builder.Build();

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
    pattern : "students/top/{count : int : range(1, 4)}",
    defaults : new{controller = "Students", action = "Top"}
);

app.MapControllerRoute(
    name : "intake",
    pattern : "students/intake/{code : intakeCode}",
    defaults : new {controller = "Students", action = "Intake"}
);
 
 app.MapControllerRoute(
    name : "default",
    pattern : "{controller=Home}/{action=Index}/{id?}"
    );

    app.Run();


    //Lab ID: 15 ,(15 mod 4) + 1 = 4  Max Year = 4 
    // 2.5 + (15 mod 3) × 0.5 = 2.5   Min Gpa = 2.5
    // 15 mod 3 = 0, iti + A = itiA   Intake Code = itiA
    //The default route is a catch all that matches any remaining request If it were at the top  all other routes would never be reached because the default would match first
    //Yes, it is acceptable because multiple URLs can serve the same action

 