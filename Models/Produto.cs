using System;
using System.Collections.Generic;

namespace Host.Models;

public partial class Produto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public decimal Preco { get; set; }

    public int SldAtual { get; set; }

    public string? Categoria { get; set; }
}
