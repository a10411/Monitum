using MonitumBOL.Models;
using MonitumDAL.AuthUtils;
using System.Data;
using System.Data.SqlClient;

namespace MonitumDAL
{
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes ao Gestor
    /// Isto é, todos os acessos à base de dados relativos ao gestor estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class GestorService
    {
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter todos os registos de gestores lá criados (tabela Gestor)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista de gestores</returns>
        public static async Task<List<Gestor>> GetAllGestores(string conString)
        {
            var gestorList = new List<Gestor>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Gestor", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Gestor gestor = new Gestor(rdr);
                    gestorList.Add(gestor);
                }
                rdr.Close();
                con.Close();
            }

            return gestorList;
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL via query e confirmar se os dados de email e password são válidos e pertencem a um gestor existente na BD (efetuar Login)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="email">Email do gestor que se pretende autenticar</param>
        /// <param name="password">Password do gestor que se pretende autenticar</param>
        /// <returns>True caso dados estejam corretos (autenticação realizada), false caso dados incorretos ou erro interno</returns>
        public static async Task<Boolean> LoginGestor(string conString, string email, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM Gestor where email = '{email}'", con);

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
            } catch
            {
                throw;
            }
            
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter o registo de um gestor, caso exista, através do email (tabela Gestor)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="email">Email do gestor que se pretende autenticar</param>
        /// <returns></returns>
        public static async Task<Gestor> GetGestorByEmail(string conString, string email)
        {
            Gestor gestor = new Gestor();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Gestor where email = '{email}'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    gestor = new Gestor(rdr);
                }
                rdr.Close();
                con.Close();
            }

            return gestor;
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL via query e adicionar um novo gestor, encriptando a sua password (registo)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="email">Email do gestor que se pretende registar</param>
        /// <param name="password">Password do gestor que se pretende registar</param>
        /// <returns>True caso gestor tenha sido introduzio, erro interno caso tenha existido algum erro</returns>
        public static async Task<Boolean> RegisterGestor(string conString, string email, string password)
        {
            string salt = HashSaltClass.GenerateSalt();
            byte[] hashedPW = HashSaltClass.GetHash(password, salt);
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
                        await GestorService.SetGestorToEstabelecimento(conString, email);
                        return true;
                    }

                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados via SQL Query e adicionar um novo registo na tabela Estabelecimento_Gestor consoante o email de um Gestor (no único estabelecimento existente com ID 1)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="email">Email do gestor a adicionar</param>
        /// <returns>True se adicionar, False se não adicionar</returns>
        public static async Task<Boolean> SetGestorToEstabelecimento(string conString, string email) 
        {
            try
            {
                Gestor gestor = await GetGestorByEmail(conString, email);

                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addGestorEstabelecimento = "INSERT INTO Estabelecimento_Gestor (id_estabelecimento,id_gestor) VALUES (@idEstabelecimento,@idGestor)";
                    using (SqlCommand queryAddGestorEstabelecimento = new SqlCommand(addGestorEstabelecimento))
                    {
                        queryAddGestorEstabelecimento.Connection = con;
                        queryAddGestorEstabelecimento.Parameters.Add("@idEstabelecimento", SqlDbType.Int).Value = 1;
                        queryAddGestorEstabelecimento.Parameters.Add("@idGestor", SqlDbType.Int).Value = gestor.IdGestor;

                        con.Open();
                        queryAddGestorEstabelecimento.ExecuteNonQuery();
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