using System.Data.SqlClient;

namespace MonitumBOL.Models
{
    public class Gestor
    {
        public int IdGestor { get; set; }
        public string Email { get; set; }
        public string Password_Hash { get; set; }
        public string Password_Salt { get; set; }

        

        public Gestor(SqlDataReader rdr)
        {
            this.IdGestor = Convert.ToInt32(rdr["id_gestor"]);
            this.Email = rdr["email"].ToString() ?? String.Empty; ;
            this.Password_Hash = rdr["password_hash"].ToString() ?? String.Empty;
            this.Password_Salt = rdr["password_salt"].ToString() ?? String.Empty;
        }
    }

    
}