using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a um Estabelecimento_Gestor (entidade associativa)
    /// Implementa a class (model) Estabelecimento_Gestor e os seus construtores
    /// </summary>
    public class Estabelecimento_Gestor
    {   
        public int IdEstabelecimento { get; set; }
        public int IdGestor { get; set; }

        public Estabelecimento_Gestor()
        {

        }

        /// <summary>
        /// Construtor que visa criar um Estabelecimento_Gestor convertendo dados obtidos a partir de um SqlDataReader
        /// Este construtor é bastante útil no DAL, onde recebemos dados da base de dados e pretendemos converte-los num objeto
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Estabelecimento_Gestor(SqlDataReader rdr)
        {
            this.IdEstabelecimento = Convert.ToInt32(rdr["id_estabelecimento"]);
            this.IdGestor = Convert.ToInt32(rdr["id_gestor"]);
        }
    }
}
