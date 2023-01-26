using System;
using System.Collections.Generic;

namespace DL;

public partial class Nivel
{
    public int IdNivel { get; set; }

    public string? NombreNivel { get; set; }

    public virtual ICollection<Ciclistum> Ciclista { get; } = new List<Ciclistum>();

    public virtual ICollection<Clase> Clases { get; } = new List<Clase>();
}
