using MonitumBLL.Utils;
using MonitumBOL.Models;
using MonitumDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBLL.Logic
{
    /// <summary>
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo a logs de métricas
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    /// </summary>
    public class Log_MetricaLogic
    {
        /// <summary>
        /// Trata da parte lógica relativa à obtenção de todos as Logs de uma sala presentes na base de dados
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <returns>Response com Status Code, mensagem e dados (Logs recebidas do DAL)</returns>
        public static async Task<Response> GetAllLogMetrica(string conString, int idSala)
        {
            Response response= new Response();
            List<Log_Metrica> logList = await MonitumDAL.Log_MetricaService.GetAllLogMetrica(conString, idSala);
            if (logList.Count != 0) 
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = logList;    
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à inserção de uma Log na base de dados
        ///  Gera uma resposta que será utilizada pela MomitumAPI para responder ao request do utilizador (POST - Log_Metrica (AddLogMetrica))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="logMetricaToAdd">Log inserida pelo sitema físico para adicionar à base de dados</param>
        /// <returns>Response com Status Code e mensagem (indicando que a log foi adicionada)</returns>

        public static async Task<Response> AddLogMetrica(string conString, Log_Metrica logMetricaToAdd)
        {
            Response response= new Response();
            try
            {
                if(await Log_MetricaService.AddLogMetrica(conString, logMetricaToAdd))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Log was added";
                }
            }
            catch(Exception e)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = e.ToString();
            }
            return response;
        }
    }
}
