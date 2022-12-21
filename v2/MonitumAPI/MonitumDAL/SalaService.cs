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
        /// Método que visa aceder à base de dados SQL Server e adicionar um registo de uma sala (adicionar uma sala relativa a um estabelecimento)
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

        public static async Task<Logs_Metricas> GetMetricaBySala(string conString, int idMetrica, int idSala)
        {
            try
            {
                var log = new Logs_Metricas();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT TOP 1 * FROM Logs_Metricas WHERE id_sala = {idSala} AND id_metrica = {idMetrica} ORDER BY id_log DESC", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        log = new Logs_Metricas(rdr);
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
    }


}
