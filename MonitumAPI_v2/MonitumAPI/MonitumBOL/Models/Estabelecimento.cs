using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a um Estabelecimento
    /// Implementa a class (model) Estabelecimento e os seus construtores
    /// </summary>
    public class Estabelecimento
    {
        public int IdEstabelecimento { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }

        public Estabelecimento()
        {

        }

        /// <summary>
        /// Construtor que visa criar um Estabelecimento convertendo dados obtidos a partir de um SqlDataReader
        /// Este construtor é bastante útil no DAL, onde recebemos dados da base de dados e pretendemos converte-los num objeto
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Estabelecimento(SqlDataReader rdr)
        {
            this.IdEstabelecimento = Convert.ToInt32(rdr["id_estabelecimento"]);
            this.Nome = rdr["nome"].ToString() ?? String.Empty;
            this.Morada = rdr["morada"].ToString() ?? String.Empty;
        }
    }
}
