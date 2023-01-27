using System;
using System.Collections.Generic;

namespace DL;

public partial class CiclistaClase
{
    public int IdRelacion { get; set; }

    public int? IdCiclista { get; set; }

    public int? IdClase { get; set; }

    public string NombreCiclista { get; set; }

    public virtual Ciclistum? IdCiclistaNavigation { get; set; }

    public virtual Clase? IdClaseNavigation { get; set; }

    public int? IdHorario { get; set; }
    public string? Descripcion { get; set; }
    public int? IdAula { get; set; }
    public string? NombreAula { get; set; }
    public string? Nombre { get; set; }
}
