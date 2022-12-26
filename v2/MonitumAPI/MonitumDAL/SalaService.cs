using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitumBOL.Models;

namespace MonitumDAL
{
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes à Sala
    /// Isto é, todos os acessos à base de dados relativos à sala estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class SalaService
    {
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e adicionar um registo de uma sala (adicionar uma sala relativa a um estabelecimento)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <returns>True caso tenha adicionado ou retorna a exceção para a camada lógica caso tenha havido algum erro</returns>
        public static async Task<Boolean> AddSala(string conString, Sala salaToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addSala = "INSERT INTO Sala (id_estabelecimento, id_estado) VALUES (@idEstabelecimento, @idEstado)";
                    using (SqlCommand queryAddSala = new SqlCommand(addSala))
                    {
                        queryAddSala.Connection = con;
                        queryAddSala.Parameters.Add("@idEstabelecimento", SqlDbType.Int).Value = salaToAdd.IdEstabelecimento;
                        queryAddSala.Parameters.Add("@idEstado", SqlDbType.Int).Value = salaToAdd.IdEstado;
                        con.Open();
                        queryAddSala.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            } catch (Exception e){
                throw;
            }
            
            
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter as salas de um estabelecimento
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="idEstabelecimento">ID do estabelecimento para o qual pretendemos ver as salas</param>
        /// <returns>Lista de salas do estabelecimento</returns>
        public static async Task<List<Sala>> GetSalasByEstabelecimento(string conString, int idEstabelecimento)
        {
            try
            {   
                var salaList = new List<Sala>();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM Sala WHERE id_estabelecimento = {idEstabelecimento}",con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Sala sala = new Sala(rdr);
                        salaList.Add(sala);
                    }
                    rdr.Close();
                    con.Close();
                }
                return salaList;
            }
            catch(Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter a última métrica relativa a uma sala (métrica "atual")
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="idMetrica">ID da métrica que o utilizador pretende visualizar (ruído, ocupação, etc.)</param>
        /// <param name="idSala">ID da sala para a qual o utilizador pretende visualizar a métrica</param>
        /// <returns>Último log da métrica pretendida</returns>
        public static async Task<Log_Metrica> GetLastMetricaBySala(string conString, int idMetrica, int idSala)
        {
            try
            {
                var log = new Log_Metrica();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT TOP 1 * FROM Logs_Metricas WHERE id_sala = {idSala} AND id_metrica = {idMetrica} ORDER BY id_log DESC", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        log = new Log_Metrica(rdr);
                    }
                    rdr.Close();
                    con.Close();
                }
                return log;
            }
            catch (Exception e) 
            {
                throw;
            }
            
        }
        public static async Task<Sala> GetSala(string conString, int idSala)
        {
            Sala sala = new Sala();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Sala where id_sala = {idSala}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    sala = new Sala(rdr);
                }
                rdr.Close();
                con.Close();
            }

            return sala;
            // retorna uma sala com id = 0 caso não encontre nenhum com este ID
        }

        public static async Task<Sala> UpdateEstadoSala(string conString, int idSala, int idEstado)
        {
            try
            {
                // UPDATE Sala set id_estado = {id_estado} where id_sala = {id_sala}
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addSala = "UPDATE Sala set id_estado = @idEstado where id_sala = @idSala";
                    using (SqlCommand queryAddSala = new SqlCommand(addSala))
                    {
                        queryAddSala.Connection = con;
                        queryAddSala.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        queryAddSala.Parameters.Add("@idSala", SqlDbType.Int).Value = idSala;
                        con.Open();
                        queryAddSala.ExecuteNonQuery();
                        con.Close();
                        return await GetSala(conString, idSala);
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



