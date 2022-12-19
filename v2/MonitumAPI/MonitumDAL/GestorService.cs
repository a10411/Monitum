using MonitumBOL.Models;
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
        /// Método que visa aceder à base de dados SQL Server e obter todos os registos de gestores lá criados (tabela Gestor)
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
    }
}