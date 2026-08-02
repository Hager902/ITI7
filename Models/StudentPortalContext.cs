using Microsoft.EntityFrameworkCore;

namespace StudentPortalWeb.Models
{
    public class StudentPortalContext : DbContext
    {
        public StudentPortalContext(DbContextOptions<StudentPortalContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
    }
}