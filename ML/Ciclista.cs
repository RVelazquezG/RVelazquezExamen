using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Ciclista
    {
        [DisplayName("Ciclista")]
        public int? IdCiclista { get; set; }
        public string? NombreCiclista { get; set; }
        public string? Direccion { get; set; }
        public int? Edad { get; set; }
        public bool? Membresia { get; set; }
        public int? IdNivel { get; set; }
        public string? Nivele { get; set; }
        public List<ML.Ciclista>? Ciclistas { get; set; }
        public List<object>? CiclistasBase { get; set; }
        public ML.Nivel? Nivel { get; set; }
    }
}
