using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    public class Sala
    {
        public int IdSala { get; set; }
        public int IdEstabelecimento { get; set; }
        public int IdEstado { get; set; }

        public Sala()
        {

        }

        public Sala(SqlDataReader rdr)
        {
            this.IdSala = Convert.ToInt32(rdr["id_sala"]);
            this.IdEstabelecimento = Convert.ToInt32(rdr["id_estabelecimento"]);
            this.IdEstado = Convert.ToInt32(rdr["id_estado"]);
        }
    }
}
