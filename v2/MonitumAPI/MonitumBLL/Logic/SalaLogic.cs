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
    /// Classe que visa a implementação da parte de Business Logic Layer relativa à Sala
    /// Estes métodos são consumidos pelo MonitumAPI (Layer API), e são responsáveis por abstrair a API de detalhes como o Business Object Layer e da obtenção dos dados no DAL
    /// </summary>
    public class SalaLogic
    {
        /// <summary>
        /// Trata da parte lógica relativa à criação de uma sala na base de dados (sala que diz respeito a um estabelecimento
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (POST - Sala)
        /// </summary>
        /// <param name="conString">Connection String da base de dados</param>
        /// <param name="salaToAdd">Parâmetros da sala adicionar (idEstabelecimento, idEstado)</param>
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

        // not finished
        public static async Task<Response> GetMetrica(string conString, int idMetrica, int idSala)
        {
            Response response = new Response();
            
            try
            {
                Metricas metricas = await MonitumDAL.SalaService.GetMetricaBySala(conString, idMetrica, idSala);
                if(metricas.IdMetrica == 0)
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Metrica was not found";
                }
                else
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Sucesso na obtenção dos dados";
                    response.Data = metricas;
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
