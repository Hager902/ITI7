using Microsoft.EntityFrameworkCore;
using StudentPortalWeb.Models;
using StudentPortalWeb.Services;

namespace StudentPortalWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StudentPortalContext>(options =>
               options.UseSqlite("Data Source=/Users/fathy/course1/ConsoleApp1/StudentPortal.db"));

            builder.Services.AddTransient<IHagerStampService, HagerStampService>();  // Lab ID: 15, 15 mod 3 = 0

            var app = builder.Build();

            app.Use(async (context, next) =>{
                var path = context.Request.Path;
                Console.WriteLine($"[START] {path}");

                if (path.ToString().Contains("/audit-15")){
                    Console.WriteLine($"[AUDIT] Hager Fathy saw a request for {path}");
                }

                await next.Invoke();

                Console.WriteLine($"[END] {path}");
            });

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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}


//"ttp://localhost:5291 hin appSettings.json
//B1 : The app compiles successfully without errors but the registration will not take effect
//B2 : The error happened on the first request
//B3 : 1
//C : 1 student
//Unable to resolve service for type 'StudentPortalWeb.Models.StudentPortalContext' while attempting to activate 'StudentPortalWeb.Controllers.HomeController'
//it started successfully but the page load failed with an error
// yes predication is right
//D : The app started successfully without errors
//The page loaded successfully 
//NO error messages appeared at all.
//This looks like everything works fine But this is actually a problem
//Bad news , DbContext is not thread safe and should never be used as a Singleton in a web application When you register DbContext as Singleton the same instance is shared across ALL requests
//E : Stamp A: 9d4211f0 | Stamp B: f2c9625e , Stamp A: 82a12966 | Stamp B: 12a67730
//No, because lifetime is transient
//Yes, because lifetime is transient
//Their lifetime is Scoped, Yes, it matched, Yes, it changed on every refresh, the opposite from mine
// 6 Starts , Because each file is a separate HTTP request
// /css/site.css, /lib/bootstrap/dist/js/bootstrap.bundle.min.js, /StudentPortalWeb.styles.css, /lib/jquery/dist/jquery.min.js, /js/site.js, /lib/bootstrap/dist/css/bootstrap.min.css
// UseStaticFiles() handles all static files first and sends the response before the middleware sees them, only the main HTML request reaches the middleware
//because the same StudentPortal class can now work with different databases, connection strings and environments
//Because the application would silently fail for one user in production and the error would be hard to diagnose 
//EF Core would not know which migration history to trust.
