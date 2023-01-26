using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Horario
    {
        public int IdHorario { get; set; }

        public string? Descripcion { get; set; }

        public List<object> Horarios { get; set; }
    }
}
