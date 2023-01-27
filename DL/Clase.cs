using System;
using System.Collections.Generic;

namespace DL;

public partial class Clase
{
    public int IdClase { get; set; }

    public string? Nombre { get; set; }

    public int? IdNivel { get; set; }

    public int? IdHorario { get; set; }

    public int? IdAula { get; set; }

    public virtual ICollection<CiclistaClase> CiclistaClases { get; } = new List<CiclistaClase>();

    public virtual Aula? IdAulaNavigation { get; set; }

    public virtual Horario? IdHorarioNavigation { get; set; }

    public virtual Nivel? IdNivelNavigation { get; set; }

    public string NombreNivel { get; set; }
    public string Descripcion { get; set; }
    public string NombreAula { get; set; }

}
