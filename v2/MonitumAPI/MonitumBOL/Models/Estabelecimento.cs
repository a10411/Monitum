using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    public class Estabelecimento
    {
        public int IdEstabelecimento { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }

        public Estabelecimento()
        {

        }

        public Estabelecimento(SqlDataReader rdr)
        {
            this.IdEstabelecimento = Convert.ToInt32(rdr["id_estabelecimento"]);
            this.Nome = rdr["nome"].ToString() ?? String.Empty;
            this.Morada = rdr["morada"].ToString() ?? String.Empty;
        }
    }
}
