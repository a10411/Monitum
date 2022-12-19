using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    internal class Comunicados
    {
        public int IdComunicado { get; set; }
        public int IdSala { get; set; }
        public string Titulo { get; set; }
        public string Corpo { get; set; }

        public DateTime DataHora { get; set; }
    }
}
