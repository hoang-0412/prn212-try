using System;
using System.Collections.Generic;

namespace Q2.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseTitle { get; set; } = null!;

    public string CourseCode { get; set; } = null!;

    public int DurationHours { get; set; }

    public decimal TuitionFee { get; set; }

    public int CategoryId { get; set; }

    public int InstructorId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Instructor Instructor { get; set; } = null!;
}
