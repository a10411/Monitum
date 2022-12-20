using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    public class Comunicados
    {
        public int IdComunicado { get; set; }
        public int IdSala { get; set; }
        public string Titulo { get; set; }
        public string Corpo { get; set; }

        public DateTime DataHora { get; set; }

        public Comunicados()
        {

        }

        public Comunicados(SqlDataReader rdr)
        {
            this.IdComunicado = Convert.ToInt32(rdr["id_comunicado"]);
            this.IdSala = Convert.ToInt32(rdr["id_sala"]);
            this.Titulo = rdr["titulo"].ToString() ?? String.Empty;
            this.Corpo = rdr["corpo"].ToString() ?? String.Empty;
            this.DataHora = Convert.ToDateTime(rdr["data_hora"].ToString()); // testar

        }
    }
}
