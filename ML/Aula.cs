using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Aula
    {
        [DisplayName("Aula")]
        public int IdAula { get; set; }

        public string? NombreAula { get; set; }

        public List<object> Aulas { get; set; }
    }
}
