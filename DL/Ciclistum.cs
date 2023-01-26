using System;
using System.Collections.Generic;

namespace DL;

public partial class Ciclistum
{
    public int IdCiclista { get; set; }

    public string? NombreCiclista { get; set; }

    public string? Direccion { get; set; }

    public int? Edad { get; set; }

    public bool? Membresia { get; set; }

    public int? IdNivel { get; set; }

    public virtual ICollection<CiclistaClase> CiclistaClases { get; } = new List<CiclistaClase>();

    public virtual Nivel? IdNivelNavigation { get; set; }

    public string? NombreNivel { get; set; }
}
