using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a uma Metrica
    /// Implementa a class (model) Metrica e os seus construtores
    /// </summary>
    public class Metrica
    {
        public int IdMetrica { get; set; }
        public string? Nome { get; set; }
        public string? Medida { get; set; }

        public Metrica()
        {

        }

        /// <summary>
        /// Construtor que visa criar uma Metrica convertendo dados obtidos a partir de um SqlDataReader
        /// Este construtor é bastante útil no DAL, onde recebemos dados da base de dados e pretendemos converte-los num objeto
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Metrica(SqlDataReader rdr)
        {
            this.IdMetrica = Convert.ToInt32(rdr["id_metrica"]);
            this.Nome = rdr["nome"].ToString() ?? String.Empty;
            this.Medida = rdr["medida"].ToString() ?? String.Empty;
        }

    }
}
