using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Clase
    {
        [DisplayName("Clase")]
        public int IdClase { get; set; }
        [DisplayName("Nombre de la clase")]
        public string? Nombre { get; set; }
        public int? IdNivel { get; set; }

        public int? IdHorario { get; set; }

        public int? IdAula { get; set; }

        public ML.Nivel Nivel { get; set; }
        public ML.Horario Horario { get; set; }
        public ML.Aula Aula { get; set; }

        public List<object> Clases { get; set; }


    }
}
