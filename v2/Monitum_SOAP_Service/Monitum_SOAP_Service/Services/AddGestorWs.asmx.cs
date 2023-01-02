using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;

namespace Monitum_SOAP_Service.Services
{
    /// <summary>
    /// Summary description for AddGestorWs
    /// Serviço para adicionar um novo gestor à base de dados
    /// </summary>
    [WebService(Namespace = "http://ipca.pt/",
                Description = "ISI2022 - MonitumWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AddGestorWs : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int CalcularSoma(int x, int y)
        {
            return x + y;
        }

        /// <summary>
        /// Web service para adicionar um gestor (registar um gestor) e adicionar o mesmo ao único estabelecimento existente
        /// </summary>
        /// <param name="emailGestor">Email do gestor a registar</param>
        /// <param name="passwordGestor">Password do gestor a registar</param>
        /// <param name="emailAdmin">Email do administrador que está a registar este gestor (apenas um administrador consegue registar um gestor)</param>
        /// <param name="passwordAdmin">Password do administrador que está a registar este gestor</param>
        /// <returns>True se adicionar, false se não adicionar</returns>
        [WebMethod(MessageName = "RegistarGestorBD",
                   Description = "Regista um gestor na BD")]
        public Boolean RegistarGestorBD(string emailGestor, string passwordGestor, string emailAdmin, string passwordAdmin)
        {
            string conString = "data source=LAPTOP-IHSTCNCH;initial catalog=MonitumDB;trusted_connection=true";
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    // Verificar Administrador
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM Administrador where email = '{emailAdmin}'", con);

                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        // Hash e salt handling
                        string hashedPWFromDb = rdr["password_hash"].ToString();
                        string saltAdmin = rdr["password_salt"].ToString();
                        rdr.Close();
                        con.Close();
                        if (!CompareHashedPasswords(passwordAdmin, hashedPWFromDb, saltAdmin))
                        {
                            return false;
                        }
                        else
                        {
                            // Adicionar Gestor
                            string salt = GenerateSalt();
                            byte[] hashedPW = GetHash(passwordGestor, salt);
                            string addGestor = "INSERT INTO Gestor (email,password_hash,password_salt) VALUES (@email, @password_hash, @password_salt)";
                            using (SqlCommand queryAddGestor = new SqlCommand(addGestor))
                            {
                                queryAddGestor.Connection = con;
                                queryAddGestor.Parameters.Add("@email", SqlDbType.VarChar).Value = emailGestor;
                                queryAddGestor.Parameters.Add("@password_hash", SqlDbType.VarChar).Value = Convert.ToBase64String(hashedPW);
                                queryAddGestor.Parameters.Add("@password_salt", System.Data.SqlDbType.VarChar).Value = salt;
                                con.Open();
                                queryAddGestor.ExecuteNonQuery();
                                con.Close();

                                // Get gestor by email
                                cmd = new SqlCommand($"SELECT * FROM Gestor where email = '{emailGestor}'", con);
                                cmd.CommandType = CommandType.Text;
                                con.Open();

                                rdr = cmd.ExecuteReader();
                                int idGestor = 0;
                                while (rdr.Read())
                                {
                                    idGestor = Convert.ToInt32(rdr["id_gestor"]);
                                }
                                rdr.Close();
                                con.Close();


                                // Set gestor to estabelecimento
                                string addGestorEstabelecimento = "INSERT INTO Estabelecimento_Gestor (id_estabelecimento,id_gestor) VALUES (@idEstabelecimento,@idGestor)";
                                using (SqlCommand queryAddGestorEstabelecimento = new SqlCommand(addGestorEstabelecimento))
                                {
                                    queryAddGestorEstabelecimento.Connection = con;
                                    queryAddGestorEstabelecimento.Parameters.Add("@idEstabelecimento", SqlDbType.Int).Value = 1;
                                    queryAddGestorEstabelecimento.Parameters.Add("@idGestor", SqlDbType.Int).Value = idGestor;

                                    con.Open();
                                    queryAddGestorEstabelecimento.ExecuteNonQuery();
                                    con.Close();
                                    return true;
                                }
                            }
                        }
                    }
                    rdr.Close();
                    con.Close();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Classe que visa gerar um random salt, que acompanhará a password para a sua encriptação e posterior desencriptação
        /// </summary>
        /// <returns>Salt gerado</returns>
        public static string GenerateSalt()
        {
            byte[] salt;
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt = new byte[16]);
                return Convert.ToBase64String(salt);
            }   
        }

        /// <summary>
        /// Gera um hash consoante a password e o salt anteriormente gerado
        /// </summary>
        /// <param name="plainPassword">Password introduzida para gerar o hash</param>
        /// <param name="Salt">Salt gerado</param>
        /// <returns>Hash gerado</returns>
        public static byte[] GetHash(string plainPassword, string Salt)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(String.Concat(Salt, plainPassword));
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(byteArray);
                return hashedBytes;
            }
        }

        /// <summary>
        /// Compara duas hashed passwords, a introduzida pelo user, e a existente na base de dados (com auxilio do Salt já existente na BD)
        /// Caso sejam iguais, retorna true, indicando que a password introduzida está correta
        /// </summary>
        /// <param name="userInputPassword">Password introduzida pelo utilizador</param>
        /// <param name="ExistingHashedBase64StringPassword">Hashed password existente na base de dados</param>
        /// <param name="SaltOnDB">Salt existente na base de dados</param>
        /// <returns>True caso iguais (password correta), false caso diferentes (password introduzida incorreta)</returns>
        public static bool CompareHashedPasswords(string userInputPassword, string ExistingHashedBase64StringPassword, string SaltOnDB)
        {
            string UserInputHashedPassword = Convert.ToBase64String(GetHash(userInputPassword, SaltOnDB));
            return ExistingHashedBase64StringPassword == UserInputHashedPassword;
        }
    }
}
