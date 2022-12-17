using MonitumBOL.Models;
using System.Data;
using MonitumDAL.Utils;
using System.Data.SqlClient;

namespace MonitumDAL
{
    public class GestorService
    {
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
                    Gestor gestor = new Gestor();

                    gestor.IdGestor = Convert.ToInt32(rdr["id_gestor"]);
                    gestor.Email = rdr["email"].ToString();
                    gestor.Password = rdr["password"].ToString();

                    gestorList.Add(gestor);


                }
                rdr.Close();
                con.Close();
            }

            return gestorList;
        }
    }
}