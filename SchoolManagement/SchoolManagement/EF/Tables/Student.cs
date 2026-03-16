using System;
using System.Collections.Generic;

namespace SchoolManagement.EF.Tables;

public partial class Student
{
    public int StudentId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public int? Age { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
