using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    public class Administrador
    {
        public int IdAdministrador { get; set; }
        public string Email { get; set; }
        public string Password_Hash { get; set; }
        public string Password_Salt { get; set; }

        public Administrador()
        {

        }

        public Administrador(SqlDataReader rdr)
        {
            this.IdAdministrador = Convert.ToInt32(rdr["id_administrador"]);
            this.Email = rdr["email"].ToString() ?? String.Empty;
            this.Password_Hash = rdr["password_hash"].ToString() ?? String.Empty;
            this.Password_Salt = rdr["password_salt"].ToString() ?? String.Empty;
        }
    }
}
