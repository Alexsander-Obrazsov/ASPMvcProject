using System;
using System.Collections.Generic;

namespace Test.Model.DataBase;

public partial class Teacher
{
    public long Id { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; } = new List<Class>();

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();
}
