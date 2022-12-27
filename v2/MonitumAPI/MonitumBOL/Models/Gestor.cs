using System.Data.SqlClient;

namespace MonitumBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a um Gestor
    /// Implementa a class (model) Gestor e os seus construtores
    /// </summary>
    public class Gestor
    {
        public int IdGestor { get; set; }
        public string Email { get; set; }
        public string Password_Hash { get; set; }
        public string Password_Salt { get; set; }

        public Gestor()
        {

        }

        /// <summary>
        /// Construtor que visa criar um Gestor convertendo dados obtidos a partir de um SqlDataReader
        /// Este construtor é bastante útil no DAL, onde recebemos dados da base de dados e pretendemos converte-los num objeto
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Gestor(SqlDataReader rdr)
        {
            this.IdGestor = Convert.ToInt32(rdr["id_gestor"]);
            this.Email = rdr["email"].ToString() ?? String.Empty;
            this.Password_Hash = rdr["password_hash"].ToString() ?? String.Empty;
            this.Password_Salt = rdr["password_salt"].ToString() ?? String.Empty;
        }
    }

    
}