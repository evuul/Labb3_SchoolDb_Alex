using System;
using System.Collections.Generic;

namespace Labb3_SchoolDb_Alex.Migrations;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public int EmployeeId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
