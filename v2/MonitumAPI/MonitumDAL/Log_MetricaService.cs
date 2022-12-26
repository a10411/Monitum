
using MonitumBOL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumDAL
{
    public class Log_MetricaService
    {


        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter as logs de métricas de uma sala
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="idSala">ID da Sala, da qual pretendemos obter as logs</param>
        /// <returns>Logs da sala</returns>
        public static async Task<List<Log_Metrica>> GetAllLogMetrica(string conString, int idSala)
        {
            var logList = new List<Log_Metrica>();  
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Logs_Metricas WHERE id_sala = {idSala}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Log_Metrica log = new Log_Metrica(rdr);
                    logList.Add(log);
                }
                rdr.Close();
                con.Close();
            }
            return logList;
        }

        /// <summary>
        ///  Método que visa aceder à base de dados SQL Server via query e adicionar uma log
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="logMetricaToAdd">Log a adicionar</param>
        /// <returns></returns>
        public static async Task<Boolean> AddLogMetrica(string conString, Log_Metrica logMetricaToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addLogMetrica = "INSERT INTO Logs_Metricas (id_sala, id_metrica, valor_metrica, data_hora) VALUES (@IdSala, @IdMetrica, @ValorMetrica, @DataHora)";
                    using (SqlCommand queryAddLogMetrica = new SqlCommand(addLogMetrica))
                    {
                        queryAddLogMetrica.Connection = con;
                        queryAddLogMetrica.Parameters.Add("@IdSala", System.Data.SqlDbType.Int).Value = logMetricaToAdd.IdSala;
                        queryAddLogMetrica.Parameters.Add("@IdMetrica", System.Data.SqlDbType.Int).Value = logMetricaToAdd.IdMetrica;
                        queryAddLogMetrica.Parameters.Add("@ValorMetrica", System.Data.SqlDbType.Int).Value = logMetricaToAdd.ValorMetrica;
                        queryAddLogMetrica.Parameters.Add("@DataHora", System.Data.SqlDbType.Int).Value = logMetricaToAdd.DataHora.TimeOfDay;
                        con.Open();
                        queryAddLogMetrica.ExecuteNonQuery();
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
