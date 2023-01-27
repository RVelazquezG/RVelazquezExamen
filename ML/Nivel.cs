using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Nivel
    {
        [DisplayName("Nivel")]
        public int? IdNivel { get; set; }

        public string? NombreNivel { get; set; }

        public List<object>? Niveles { get; set; }
    }
}
