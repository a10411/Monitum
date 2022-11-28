using System;
using System.Collections.Generic;

#nullable disable

namespace MonitumAPI.Models
{
    public partial class Metrica
    {
        public Metrica()
        {
            LogsMetricas = new HashSet<LogsMetrica>();
        }

        public int IdMetrica { get; set; }
        public string Nome { get; set; }
        public string Medida { get; set; }

        public virtual ICollection<LogsMetrica> LogsMetricas { get; set; }
    }
}
