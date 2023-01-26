using System;
using System.Collections.Generic;

namespace DL;

public partial class Horario
{
    public int IdHorario { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Clase> Clases { get; } = new List<Clase>();
}
