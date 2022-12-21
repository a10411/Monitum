using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a um Log_Metrica (Log de uma Métrica)
    /// Implementa a class (model) Log_Metrica e os seus construtores
    /// </summary>
    public class Log_Metrica
    {
        public int IdLog { get; set; }
        public int IdSala { get; set; }
        public int IdMetrica { get; set; }
        public int ValorMetrica { get; set; }
        public DateTime DataHora { get; set; }

        public Log_Metrica()
        {

        }

        /// <summary>
        /// Construtor que visa criar um Log_Metrica convertendo dados obtidos a partir de um SqlDataReader
        /// Este construtor é bastante útil no DAL, onde recebemos dados da base de dados e pretendemos converte-los num objeto
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Log_Metrica(SqlDataReader rdr)
        {
            this.IdLog = Convert.ToInt32(rdr["id_log"]);
            this.IdSala = Convert.ToInt32(rdr["id_sala"]);
            this.IdMetrica = Convert.ToInt32(rdr["id_metrica"]);
            this.ValorMetrica = Convert.ToInt32(rdr["valor_metrica"]);
            this.DataHora = Convert.ToDateTime(rdr["data_hora"].ToString()); // testar
        }

    }
}
