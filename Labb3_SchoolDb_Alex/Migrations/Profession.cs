using System;
using System.Collections.Generic;

namespace Labb3_SchoolDb_Alex.Migrations;

public partial class Profession
{
    public int ProfessionId { get; set; }

    public string ProfessionName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
