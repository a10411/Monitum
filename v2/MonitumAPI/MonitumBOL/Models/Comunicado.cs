using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a um Comunicado
    /// Implementa a class (model) Comunicado e os seus construtores
    /// </summary>
    public class Comunicado
    {
        public int IdComunicado { get; set; }
        public int IdSala { get; set; }
        public string Titulo { get; set; }
        public string Corpo { get; set; }

        public DateTime DataHora { get; set; }

        public Comunicado()
        {

        }

        /// <summary>
        /// Construtor que visa criar um Comunicado convertendo dados obtidos a partir de um SqlDataReader
        /// Este construtor é bastante útil no DAL, onde recebemos dados da base de dados e pretendemos converte-los num objeto
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Comunicado(SqlDataReader rdr)
        {
            this.IdComunicado = Convert.ToInt32(rdr["id_comunicado"]);
            this.IdSala = Convert.ToInt32(rdr["id_sala"]);
            this.Titulo = rdr["titulo"].ToString() ?? String.Empty;
            this.Corpo = rdr["corpo"].ToString() ?? String.Empty;
            this.DataHora = Convert.ToDateTime(rdr["data_hora"].ToString()); // testar

        }
    }
}
