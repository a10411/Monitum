using System;
using System.Collections.Generic;

#nullable disable

namespace MonitumAPI.Models
{
    public partial class LogsMetrica
    {
        public int IdLog { get; set; }
        public int IdSala { get; set; }
        public int IdMetrica { get; set; }
        public int ValorMetrica { get; set; }
        public TimeSpan DataHora { get; set; }

        public virtual Metrica IdMetricaNavigation { get; set; }
        public virtual Sala IdSalaNavigation { get; set; }
    }
}
