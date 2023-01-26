using System;
using System.Collections.Generic;

namespace DL;

public partial class CiclistaClase
{
    public int IdRelacion { get; set; }

    public int? IdCiclista { get; set; }

    public int? IdClase { get; set; }

    public virtual Ciclistum? IdCiclistaNavigation { get; set; }

    public virtual Clase? IdClaseNavigation { get; set; }
}
