using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    public class Logs_Metricas
    {
        public int IdLog { get; set; }
        public int IdSala { get; set; }
        public int IdMetrica { get; set; }
        public int ValorMetrica { get; set; }
        public DateTime DataHora { get; set; }

    }
}
