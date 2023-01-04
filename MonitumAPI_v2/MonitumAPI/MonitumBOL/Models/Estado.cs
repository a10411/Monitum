using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a um Estado
    /// Implementa a class (model) Estado e os seus construtores
    /// </summary>
    public class Estado
    {
        public int IdEstado { get; set; }
        public string Nome_Estado { get; set; }

        public Estado()
        {

        }

        /// <summary>
        /// Construtor que visa criar um Estado convertendo dados obtidos a partir de um SqlDataReader
        /// Este construtor é bastante útil no DAL, onde recebemos dados da base de dados e pretendemos converte-los num objeto
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Estado(SqlDataReader rdr)
        {
            this.IdEstado = Convert.ToInt32(rdr["id_estado"]);
            this.Nome_Estado = rdr["estado"].ToString() ?? String.Empty;
        }

    }
}
