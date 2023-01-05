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
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo às métricas
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    /// </summary>
    public class MetricaLogic
    {

        /// <summary>
        /// Trata da parte lógica relativa à visualização de todas as métricas presentes na base de dados
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <returns>Response com Status Code, mensagem e dados (lista de métricas)</returns>
        public static async Task<Response> GetMetricas(string conString)
        {
            Response response = new Response();
            List<Metrica> metricasList = await MetricaService.GetAllMetricas(conString);
            if (metricasList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = metricasList;
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à inserção de uma métrica na base de dados
        /// Gera uma resposta que será utilizada pela MomitumAPI para responder ao request do utilizador (POST - Metrica (AddMetrica))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="metricaToAdd">Métrica inserida pelo gestor para adicionar à base de dados</param>
        /// <returns>Response com Status Code e mensagem (indicando que a métrica foi adicionada)</returns>
        public static async Task<Response> AddMetrica(string conString, Metrica metricaToAdd)
        {
            Response response= new Response();
            try
            {
                if(await MetricaService.AddMetrica(conString, metricaToAdd))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Metrica was added";
                }
            }
            catch(Exception e) 
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = e.ToString();
            }
            return response;
        }
        /// <summary>
        /// Trata da parte lógica relativa à substituição de uma métrica na base de dados
        /// Gera uma resposta que será utilizada pela MomitumAPI para responder ao request do utilizador (PUT - Metrica (PutMetrica))</summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="metricaToUpdate">Métrica inserida pelo gestor para substituir na base de dados</param>
        /// <returns>Response com Status Code, mensagem (indicando que a métrica foi substituída) e dados (métrica atualizada (nova))</returns>
        public static async Task<Response> PutMetrica(string conString, Metrica metricaToUpdate)
        {
            Response response= new Response();
            try
            {
                Metrica metricaReturned = await MetricaService.PutMetrica(conString, metricaToUpdate);
                if(metricaReturned.IdMetrica == 0)
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Metrica was not found.";
                }
                else
                {
                    response.StatusCode= StatusCodes.SUCCESS;
                    response.Message = "Metrica was swapped.";
                    response.Data = metricaReturned;
                }
            }
            catch(Exception e)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = e.ToString();
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à atualização de uma métrica na base de dados
        /// Gera uma resposta que será utilizada pela MomitumAPI para responder ao request do utilizador (PATCH - Metrica (UpdateMetrica))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="metricaToUpdate">Métrica inserida pelo gestor para atualizar na base de dados</param>
        /// <returns>Response com Status Code, mensagem (indicando que a métrica foi atualizada) e dados (métrica atualizada)</returns>
        public static async Task<Response> UpdateMetrica(string conString, Metrica metricaToUpdate)
        {
            Response response = new Response();
            try
            {
                Metrica metricaReturned = await MetricaService.UpdateMetrica(conString, metricaToUpdate);
                if (metricaReturned.IdMetrica == 0)
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Metrica was not found.";
                }
                else
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Metrica was updated.";
                    response.Data = metricaReturned;
                }
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = e.ToString();
            }
            return response;
        }


    }
}
