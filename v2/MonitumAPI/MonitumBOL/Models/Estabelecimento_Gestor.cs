using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    public class Estabelecimento_Gestor
    {   
        public int IdEstabelecimento { get; set; }
        public int IdGestor { get; set; }

        public Estabelecimento_Gestor()
        {

        }

        public Estabelecimento_Gestor(SqlDataReader rdr)
        {
            this.IdEstabelecimento = Convert.ToInt32(rdr["id_estabelecimento"]);
            this.IdGestor = Convert.ToInt32(rdr["id_gestor"]);
        }
    }
}
