using System;
using System.Collections.Generic;

#nullable disable

namespace MonitumAPI.Models
{
    public partial class Sala
    {
        public Sala()
        {
            HorarioSalas = new HashSet<HorarioSala>();
            LogsMetricas = new HashSet<LogsMetrica>();
        }

        public int IdSala { get; set; }
        public int IdEstabelecimento { get; set; }
        public int IdEstado { get; set; }

        public virtual Estabelecimento IdEstabelecimentoNavigation { get; set; }
        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual ICollection<HorarioSala> HorarioSalas { get; set; }
        public virtual ICollection<LogsMetrica> LogsMetricas { get; set; }
    }
}
