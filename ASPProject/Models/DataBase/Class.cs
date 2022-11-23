using System;
using System.Collections.Generic;

namespace Test.Model.DataBase;

public partial class Class
{
    public long Id { get; set; }

    public string ClassName { get; set; } = null!;

    public long TeacherId { get; set; }

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();

    public virtual Teacher Teacher { get; set; } = null!;
}
