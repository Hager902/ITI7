using System.ComponentModel.DataAnnotations;

namespace StudentPortalWeb.Models
{

    //Range is a validation attribute, not a schema
    //it does not affect the database and we can remove Range and the database column stays unchanged, but validation stops

    public class Student
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string? FullName { get; set; }
        
        public string? Email { get; set; }

        public string IntakeCode { get; set; } = string.Empty;

        // Lab ID 15: MAX YEAR = (15 mod 3) + 2 = 2
        [Range(1, 2 , ErrorMessage = "Year of study must be between 1 and 2.")]
        public int YearOfStudy { get; set; }

        // Lab ID 15: MIN GPA = 2.0 + (15 mod 5) × 0.3 = 2.0
        [Range(2.0, 4.0, ErrorMessage = "GPA must be between 2.0 and 4.0.")]
        public double Gpa { get; set; }

    }
}