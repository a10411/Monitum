using System;
using System.Collections.Generic;

#nullable disable

namespace MonitumAPI.Models
{
    public partial class HorarioSala
    {
        public int IdHorario { get; set; }
        public int IdSala { get; set; }
        public string DiaSemana { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSaida { get; set; }

        public virtual Sala IdSalaNavigation { get; set; }
    }
}
