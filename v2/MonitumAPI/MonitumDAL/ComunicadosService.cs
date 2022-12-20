using MonitumBOL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumDAL
{
    public class ComunicadosService
    {
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server e obter todos os registos de comunicados lá criados (tabela Comunicados)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista de comunicados</returns>
        public static async Task<List<Comunicados>> GetAllComunicados(string conString)
        {
            var comunicadosList = new List<Comunicados>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Comunicados", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Comunicados comunicado = new Comunicados(rdr);
                    comunicadosList.Add(comunicado);
                }
                rdr.Close();
                con.Close();
            }

            return comunicadosList;
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server e adicionar um registo de um comunicado
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <returns>True caso tenha adicionado ou retorna a exceção para a camada lógica caso tenha havido algum erro</returns>
        public static async Task<Boolean> AddComunicado(string conString, Comunicados comunicadoToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addComunicado = "INSERT INTO Comunicados (id_sala,titulo,corpo,data_hora) VALUES (@idSala, @titulo, @corpo, @dataHora)";
                    using (SqlCommand queryAddComunicado = new SqlCommand(addComunicado))
                    {
                        queryAddComunicado.Connection = con;
                        queryAddComunicado.Parameters.Add("@idSala", SqlDbType.Int).Value = comunicadoToAdd.IdSala;
                        queryAddComunicado.Parameters.Add("@titulo", SqlDbType.Char).Value = comunicadoToAdd.Titulo;
                        queryAddComunicado.Parameters.Add("@corpo", SqlDbType.Char).Value = comunicadoToAdd.Corpo;
                        queryAddComunicado.Parameters.Add("@dataHora", SqlDbType.Time).Value = comunicadoToAdd.DataHora.TimeOfDay;
                        con.Open();
                        queryAddComunicado.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }


        }
    }
}
