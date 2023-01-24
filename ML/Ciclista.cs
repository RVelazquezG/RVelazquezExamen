using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Ciclista
    {
        public int IdCiclista { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Edad { get; set; }
        public string Nivel { get; set; }
        public bool MembresiaActiva { get; set; }
        public List<object> Ciclistas { get; set; }
    }
}
