using System.ComponentModel.DataAnnotations;

public class CourseModel
{
    public int Id { get; set; }

    // ✅ Built-in + Custom annotation together
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be 3-100 chars")]
    [NoBadWords]                          // 👈 YOUR custom annotation
    [Display(Name = "Course Title")]
    public string Title { get; set; }

    // ✅ Built-in annotation
    [Required(ErrorMessage = "Credit is required")]
    [Range(1, 4, ErrorMessage = "Credit must be between 1 and 4")]
    [Display(Name = "Credit Hours")]
    public int Credit { get; set; }

    // ✅ Custom annotation
    [Required(ErrorMessage = "Semester is required")]
    [SemesterFormat]                      // 👈 YOUR custom annotation
    [Display(Name = "Semester")]
    public string Semester { get; set; }

    // ✅ Custom annotation with parameter
    [Required(ErrorMessage = "Course Code is required")]
    [CourseCode("CS")]                    // 👈 YOUR custom annotation with param
    [Display(Name = "Course Code")]
    public string CourseCode { get; set; }
}