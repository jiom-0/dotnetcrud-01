using System;
using System.Collections.Generic;

namespace Host.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Passwd { get; set; } = null!;

    public string? Permission { get; set; }
}
