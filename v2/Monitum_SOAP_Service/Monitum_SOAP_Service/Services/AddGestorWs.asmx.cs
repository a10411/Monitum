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

        [WebMethod]
        public Boolean RegistarGestorCopy(string email, string password)
        {
            string conString = "data source=LAPTOP-IHSTCNCH;initial catalog=MonitumDB;trusted_connection=true";
            string salt = GenerateSalt();
            byte[] hashedPW = GetHash(password, salt);
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addGestor = "INSERT INTO Gestor (email,password_hash,password_salt) VALUES (@email, @password_hash, @password_salt)";
                    using (SqlCommand queryAddGestor = new SqlCommand(addGestor))
                    {
                        queryAddGestor.Connection = con;
                        queryAddGestor.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                        queryAddGestor.Parameters.Add("@password_hash", SqlDbType.VarChar).Value = Convert.ToBase64String(hashedPW);
                        queryAddGestor.Parameters.Add("@password_salt", System.Data.SqlDbType.VarChar).Value = salt;
                        con.Open();
                        queryAddGestor.ExecuteNonQuery();
                        con.Close();
                        //await GestorService.SetGestorToEstabelecimento(conString, email);
                        return true;
                    }

                }
            }
            catch
            {
                throw;
            }
        }

        // TO-DO
        [WebMethod]
        public string RegistarGestorUsingAPI(string email, string password)
        {
            // passar para post de registar gestor!
            // depois disso, tirar o comment do authorize no gestor controller (GET)
            var url = "https://localhost:7225/Gestor";
            var request = WebRequest.Create(url);
            request.Method = "GET";

            using (var webResponse = request.GetResponse())
            {
                using (var webStream = webResponse.GetResponseStream())
                {
                    using (var reader = new StreamReader(webStream))
                    {
                        var data = reader.ReadToEnd();
                        return data.ToString();
                    }
                }
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
    }
}
