using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a uma Sala
    /// Implementa a class (model) Sala e os seus construtores
    /// </summary>
    public class Sala
    {
        public int IdSala { get; set; }

        public string Nome { get; set; }
        public int IdEstabelecimento { get; set; }
        public int IdEstado { get; set; }

        public Sala()
        {

        }

        /// <summary>
        /// Construtor que visa criar uma Sala convertendo dados obtidos a partir de um SqlDataReader
        /// Este construtor é bastante útil no DAL, onde recebemos dados da base de dados e pretendemos converte-los num objeto
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Sala(SqlDataReader rdr)
        {
            this.IdSala = Convert.ToInt32(rdr["id_sala"]);
            this.Nome = rdr["nome"].ToString() ?? String.Empty;
            this.IdEstabelecimento = Convert.ToInt32(rdr["id_estabelecimento"]);
            this.IdEstado = Convert.ToInt32(rdr["id_estado"]);
        }
    }
}
