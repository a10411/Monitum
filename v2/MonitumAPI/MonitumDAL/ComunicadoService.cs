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
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes aos Comunicados
    /// Isto é, todos os acessos à base de dados relativos aos comunicados estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class ComunicadoService
    {
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter todos os registos de comunicados lá criados (tabela Comunicados)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista de comunicados</returns>
        public static async Task<List<Comunicado>> GetAllComunicados(string conString)
        {
            var comunicadosList = new List<Comunicado>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Comunicado", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Comunicado comunicado = new Comunicado(rdr);
                    comunicadosList.Add(comunicado);
                }
                rdr.Close();
                con.Close();
            }

            return comunicadosList;
        }

        public static async Task<Comunicado> GetComunicado(string conString, int idComunicado)
        {
            Comunicado comunicado = new Comunicado();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Comunicado where id_comunicado = {idComunicado}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    comunicado = new Comunicado(rdr);
                }
                rdr.Close();
                con.Close();
            }

            return comunicado;
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e adicionar um registo de um comunicado
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <returns>True caso tenha adicionado ou retorna a exceção para a camada lógica caso tenha havido algum erro</returns>
        public static async Task<Boolean> AddComunicado(string conString, Comunicado comunicadoToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addComunicado = "INSERT INTO Comunicado (id_sala,titulo,corpo,data_hora) VALUES (@idSala, @titulo, @corpo, @dataHora)";
                    using (SqlCommand queryAddComunicado = new SqlCommand(addComunicado))
                    {
                        queryAddComunicado.Connection = con;
                        queryAddComunicado.Parameters.Add("@idSala", SqlDbType.Int).Value = comunicadoToAdd.IdSala;
                        queryAddComunicado.Parameters.Add("@titulo", SqlDbType.Char).Value = comunicadoToAdd.Titulo;
                        queryAddComunicado.Parameters.Add("@corpo", SqlDbType.Char).Value = comunicadoToAdd.Corpo;
                        queryAddComunicado.Parameters.Add("@dataHora", SqlDbType.DateTime).Value = DateTime.Now.ToString();
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

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e atualizar o registo de um comunicado (atualizar um comunicado relativo a uma sala)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="comunicadoUpdated">Comunicado a atualizar</param>
        /// <returns>Comunicado atualizado ou erro</returns>
        public static async Task<Comunicado> UpdateComunicado(string conString, Comunicado comunicadoUpdated)
        {
            Comunicado comunicadoAtual = await GetComunicado(conString, comunicadoUpdated.IdComunicado);
            comunicadoUpdated.IdComunicado = comunicadoUpdated.IdComunicado != 0 ? comunicadoUpdated.IdComunicado : comunicadoAtual.IdComunicado;
            comunicadoUpdated.IdSala = comunicadoUpdated.IdSala != 0 ? comunicadoUpdated.IdSala : comunicadoAtual.IdSala;
            comunicadoUpdated.Titulo = comunicadoUpdated.Titulo != String.Empty && comunicadoUpdated.Titulo != null ? comunicadoUpdated.Titulo : comunicadoAtual.Titulo;
            comunicadoUpdated.Corpo = comunicadoUpdated.Corpo != String.Empty && comunicadoUpdated.Corpo != null ? comunicadoUpdated.Corpo : comunicadoAtual.Corpo;
            try
            {
                using(SqlConnection con = new SqlConnection(conString))
                {
                    string updateComunicado = "UPDATE Comunicado SET titulo = @titulo, corpo = @corpo where id_comunicado = @idComunicado";
                    using (SqlCommand queryUpdateComunicado = new SqlCommand(updateComunicado))
                    {
                        queryUpdateComunicado.Connection= con;
                        queryUpdateComunicado.Parameters.Add("@titulo", SqlDbType.VarChar).Value = comunicadoUpdated.Titulo;
                        queryUpdateComunicado.Parameters.Add("@corpo", SqlDbType.VarChar).Value = comunicadoUpdated.Corpo;
                        queryUpdateComunicado.Parameters.Add("@idComunicado", SqlDbType.VarChar).Value = comunicadoUpdated.IdComunicado;
                        con.Open();
                        queryUpdateComunicado.ExecuteNonQuery();
                        con.Close();
                        return await GetComunicado(conString, comunicadoUpdated.IdComunicado);
                    }
                }
            }
            catch(Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e apagar um comunicado existente na mesma
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="idComunicado">ID do comunicado a apagar</param>
        /// <returns>True caso tudo tenha corrido bem (comunicado removido), algum erro caso o comunicado não tenha sido removido.</returns>

        public static async Task<Boolean> DeleteComunicado(string conString, int idComunicado)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(conString))
                {
                    string deleteComunicado = $"DELETE FROM Comunicado WHERE id_comunicado = {idComunicado}";
                    using (SqlCommand queryDeleteComunicado = new SqlCommand(deleteComunicado)) 
                    {
                        queryDeleteComunicado.Connection= con;
                        con.Open();
                        queryDeleteComunicado.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
