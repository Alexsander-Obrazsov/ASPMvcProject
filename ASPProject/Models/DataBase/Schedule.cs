using System;
using System.Collections.Generic;

namespace Test.Model.DataBase;

public partial class Schedule
{
    public long Id { get; set; }

    public byte[] Date { get; set; } = null!;

    public string Lesson { get; set; } = null!;

    public long ClassId { get; set; }

    public long TeachersId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Teacher Teachers { get; set; } = null!;
}
