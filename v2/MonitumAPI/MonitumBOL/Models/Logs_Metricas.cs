using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public Logs_Metricas()
        {

        }

        public Logs_Metricas(SqlDataReader rdr)
        {
            this.IdLog = Convert.ToInt32(rdr["id_log"]);
            this.IdSala = Convert.ToInt32(rdr["id_sala"]);
            this.IdMetrica = Convert.ToInt32(rdr["id_metrica"]);
            this.ValorMetrica = Convert.ToInt32(rdr["valor_metrica"]);
            this.DataHora = Convert.ToDateTime(rdr["data_hora"]); // testar
        }

    }
}
