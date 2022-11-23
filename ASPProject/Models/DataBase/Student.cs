using System;
using System.Collections.Generic;

namespace Test.Model.DataBase;

public partial class Student
{
    public long Id { get; set; }

    public string FullName { get; set; } = null!;

    public long ClassId { get; set; }

    public virtual Class Class { get; set; } = null!;
}
