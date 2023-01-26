using System;
using System.Collections.Generic;

namespace DL;

public partial class Aula
{
    public int IdAula { get; set; }

    public string? NombreAula { get; set; }

    public virtual ICollection<Clase> Clases { get; } = new List<Clase>();
}
