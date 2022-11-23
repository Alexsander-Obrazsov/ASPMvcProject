using System;
using System.Collections.Generic;

namespace Test.Model.DataBase;

public partial class StudentsLogin
{
    public long StudentsId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}
