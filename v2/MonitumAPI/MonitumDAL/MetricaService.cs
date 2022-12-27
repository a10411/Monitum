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
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes à Métrica
    /// Isto é, todos os acessos à base de dados relativos à métrica estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class MetricaService
    {

        public static async Task<Metrica> GetMetrica(string conString, int idMetrica)
        {
            Metrica metrica = new Metrica();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Metricas where id_metrica = {idMetrica}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    metrica = new Metrica(rdr);    
                }
                rdr.Close();
                con.Close();
            }
            return metrica;
        }



        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e adicionar uma Metrica a uma sala de um estabelecimento
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json </param>
        /// <param name="metricaToAdd">Métrica a adicionar</param>
        /// <returns></returns>
        public static async Task<Boolean> AddMetrica(string conString, Metrica metricaToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addMetrica = "INSERT INTO Metricas (nome,medida) VALUES (@nome, @medida)"; // AINDA VOU DAR FIX, METER METRICA POR ESTABELECIMENTO E POR SALA BRB
                    using (SqlCommand queryAddMetrica = new SqlCommand(addMetrica))
                    {
                        queryAddMetrica.Connection = con;
                        queryAddMetrica.Parameters.Add("@nome", System.Data.SqlDbType.VarChar).Value = metricaToAdd.Nome;
                        queryAddMetrica.Parameters.Add("@medida", System.Data.SqlDbType.VarChar).Value = metricaToAdd.Medida;
                        con.Open();
                        queryAddMetrica.ExecuteNonQuery();
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
        /// Método que visa aceder à base de dados SQL Server via query e atualizar uma Metrica a uma sala de um estabelecimento
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="metricaToUpdated">Métrica a atualizar</param>
        /// <returns>Métrica atualizada</returns>
        public static async Task<Metrica> PutMetrica(string conString, Metrica metricaToUpdated)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(conString))
                {
                    string updateMetrica = ("UPDATE Metricas SET nome = @nome, medida = @medida");
                    using(SqlCommand queryUpdateMetrica = new SqlCommand(updateMetrica))
                    {
                        queryUpdateMetrica.Connection = con;
                        queryUpdateMetrica.Parameters.Add("@nome", SqlDbType.VarChar).Value = metricaToUpdated.Nome;
                        queryUpdateMetrica.Parameters.Add("@medida", SqlDbType.VarChar).Value= metricaToUpdated.Medida;
                        con.Open() ;
                        queryUpdateMetrica.ExecuteNonQuery();
                        con.Close();
                        return await GetMetrica(conString, metricaToUpdated.IdMetrica);
                    }
                }
            }
            catch(Exception e) 
            { 
                throw; 
            }
        }

        public static async Task<Metrica> UpdateMetrica(string conString, Metrica metricaUpdated)
        {
            
            Metrica metricaAtual = await GetMetrica(conString, metricaUpdated.IdMetrica);
            metricaUpdated.IdMetrica = metricaUpdated.IdMetrica != 0 ? metricaUpdated.IdMetrica : metricaAtual.IdMetrica;
            metricaUpdated.Nome = metricaUpdated.Nome != String.Empty && metricaUpdated.Nome != null ? metricaUpdated.Nome : metricaAtual.Nome;
            metricaUpdated.Medida= metricaUpdated.Medida != String.Empty && metricaUpdated.Medida != null ? metricaUpdated.Medida : metricaAtual.Medida;

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string updateMetrica = "UPDATE Metricas SET nome = @nome, medida = @medida where id_metrica = @idMetrica";
                    using (SqlCommand queryUpdateMetrica = new SqlCommand(updateMetrica))
                    {
                        queryUpdateMetrica.Connection = con;
                        queryUpdateMetrica.Parameters.Add("@nome", SqlDbType.VarChar).Value = metricaUpdated.Nome;
                        queryUpdateMetrica.Parameters.Add("@medida", SqlDbType.VarChar).Value = metricaUpdated.Medida;
                        queryUpdateMetrica.Parameters.Add("@idMetrica", SqlDbType.Int).Value = metricaUpdated.IdMetrica;
                        con.Open();
                        queryUpdateMetrica.ExecuteNonQuery();
                        con.Close();
                        return await GetMetrica(conString, metricaUpdated.IdMetrica);
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
