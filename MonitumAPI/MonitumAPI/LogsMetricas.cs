using Microsoft.AspNetCore.Identity;

namespace MonitumAPI
{
    public class LogsMetricas
    {
        public int IdLog { get; set; }
        public int IdSala { get; set; }
        public int IdMetrica { get; set; }
        public int ValorMetrica { get; set; }
        public DateTime time { get; set; }

        public virtual Sala IdSalaNavigation { get; set; }
        public virtual Metricas IdMetricaNavigation { get; set; }


    }
}
