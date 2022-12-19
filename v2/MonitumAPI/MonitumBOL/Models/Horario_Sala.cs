using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    internal class Horario_Sala
    {
        public int IdHorario { get; set; }
        public int IdSala { get; set; }
        public string DiaSemana { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSaida { get; set; }

    }
}
