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



        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e adicionar um horário a uma sala de um estabelecimento
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

    }









}
