using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    public class Metricas
    {
        public int IdMetrica { get; set; }
        public string Nome { get; set; }
        public string Medida { get; set; }

        public Metricas(SqlDataReader rdr)
        {
            this.IdMetrica = Convert.ToInt32(rdr["id_metrica"]);
            this.Nome = rdr["nome"].ToString() ?? String.Empty;
            this.Medida = rdr["medida"].ToString() ?? String.Empty;
        }

    }
}
