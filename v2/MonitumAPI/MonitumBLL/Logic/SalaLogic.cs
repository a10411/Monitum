using Microsoft.AspNetCore.Mvc;
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
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo às salas
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    /// </summary>
    public class SalaLogic
    {
        /// <summary>
        /// Trata da parte lógica relativa à criação de uma sala na base de dados (sala que diz respeito a um estabelecimento)
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (POST - Sala (AddSalaToEstabelecimento))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="salaToAdd">Sala a adicionar (idEstabelecimento, idEstado)</param>
        /// <returns>Response com Status Code e mensagem (Status Code 200 caso sucesso, ou 500 INTERNAL SERVER ERROR caso tenha havido algum erro</returns>
        public static async Task<Response> AddSalaToEstabelecimento(string conString, Sala salaToAdd)
        {
            Response response = new Response();
            try
            {
                if (await SalaService.AddSala(conString, salaToAdd))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Sala was added to estabelecimento.";
                }
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = e.ToString();
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à obtenção de todas as salas de um estabelecimento, dados estes que residem na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (GET - Salas (GetSalasByEstabelecimento))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="idEstabelecimento">ID do estabelecimento para o qual queremos visualizar as salas</param>
        /// <returns>Response com Status Code, mensagem e dados (Lista de salas)</returns>
        public static async Task<Response> GetSalas(string conString, int idEstabelecimento)
        {
            Response response = new Response();
            List<Sala> salaList = await MonitumDAL.SalaService.GetSalasByEstabelecimento(conString, idEstabelecimento);
            if(salaList.Count != 0 ) 
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = salaList;
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à obtenção do último log de uma métrica de uma sala, dados estes que residem na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (GET - Salas (GetLastMetricaBySala))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="idMetrica">ID da métrica que o utilizador pretende visualizar</param>
        /// <param name="idSala">ID da sala para a qual o utilizador pretende visualizar o último log da métrica</param>
        /// <returns>Response com Status Code, mensagem e dados (última log)</returns>
        public static async Task<Response> GetLastMetricaBySala(string conString, int idMetrica, int idSala)
        {
            Response response = new Response();
            
            try
            {

                Log_Metrica log = await SalaService.GetLastMetricaBySala(conString, idMetrica, idSala);
                if(log.IdLog == 0)
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Metrica was not found";
                }
                else
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Sucesso na obtenção dos dados";
                    response.Data = log;
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
