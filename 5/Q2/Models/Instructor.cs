using System;
using System.Collections.Generic;

namespace Q2.Models;

public partial class Instructor
{
    public int InstructorId { get; set; }

    public string InstructorName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
