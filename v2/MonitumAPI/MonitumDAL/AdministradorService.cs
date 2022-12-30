using MonitumDAL.AuthUtils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumDAL
{
    public class AdministradorService
    {
        public static async Task<Boolean> LoginAdministrador(string conString, string email, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM Administrador where email = '{email}'", con);

                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        // Hash e salt handling
                        string hashedPWFromDb = rdr["password_hash"].ToString();
                        string salt = rdr["password_salt"].ToString();
                        rdr.Close();
                        con.Close();
                        if (HashSaltClass.CompareHashedPasswords(password, hashedPWFromDb, salt))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    rdr.Close();
                    con.Close();
                    return false;
                }
            }
            catch
            {
                throw;
            }

        }
        public static async Task<Boolean> RegisterAdministrador(string conString, string email, string password)
        {
            string salt = HashSaltClass.GenerateSalt();
            byte[] hashedPW = HashSaltClass.GetHash(password, salt);
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addAdministrador = "INSERT INTO Administrador (email,password_hash,password_salt) VALUES (@email, @password_hash, @password_salt)";
                    using (SqlCommand queryAddAdministrador = new SqlCommand(addAdministrador))
                    {
                        queryAddAdministrador.Connection = con;
                        queryAddAdministrador.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                        queryAddAdministrador.Parameters.Add("@password_hash", SqlDbType.VarChar).Value = Convert.ToBase64String(hashedPW);
                        queryAddAdministrador.Parameters.Add("@password_salt", System.Data.SqlDbType.VarChar).Value = salt;
                        con.Open();
                        queryAddAdministrador.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }

                }
            }
            catch
            {
                throw;
            }
        }
    }
}
