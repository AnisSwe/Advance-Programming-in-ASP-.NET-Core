using System;
using System.Collections.Generic;

namespace SchoolManagement.EF.Tables;

public partial class Course
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public int? Credits { get; set; }

    public string? Department { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
