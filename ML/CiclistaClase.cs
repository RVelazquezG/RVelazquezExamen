using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class CiclistaClase
    {
        public int IdRelacion { get; set; }

        public int? IdCiclista { get; set; }

        public int? IdClase { get; set; }
        public List<object> CiclistaClases { get; set; }

        public ML.Ciclista Ciclista { get; set; }
        public ML.Clase Clase { get; set; }
    }
}
