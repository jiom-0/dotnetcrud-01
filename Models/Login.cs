using System;
using System.Collections.Generic;

namespace Host.Models;

public partial class Login
{
    public string Email { get; set; } = null!;

    public string Passwd { get; set; } = null!;
}
