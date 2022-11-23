using System;
using System.Collections.Generic;

namespace Test.Model.DataBase;

public partial class TeachersLogin
{
    public long TeachersId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}
