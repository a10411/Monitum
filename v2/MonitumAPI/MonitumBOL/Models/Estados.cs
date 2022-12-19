using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    public class Estados
    {
        public int IdEstado { get; set; }
        public string Estado { get; set; }

        public Estados(SqlDataReader rdr)
        {
            this.IdEstado = Convert.ToInt32(rdr["id_estado"]);
            this.Estado = rdr["estado"].ToString() ?? String.Empty;
        }

    }
}
