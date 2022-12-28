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
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo aos comunicados
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    /// </summary>
    public class ComunicadoLogic
    {
        /// <summary>
        /// Trata da parte lógica relativa à obtenção de todos os comunicados na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (GET - Comunicado)
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <returns>Response com Status Code, dados (lista de comunicados) e mensagem (Status Code 200 caso sucesso, ou 500 INTERNAL SERVER ERROR caso tenha havido algum erro, etc)</returns>
        public static async Task<Response> GetComunicados(string conString)
        {
            Response response = new Response();
            List<Comunicado> comunicadosList = await ComunicadoService.GetAllComunicados(conString);
            if (comunicadosList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = comunicadosList;
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à criação de um comunicado na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (POST - Comunicado)
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="comunicadoToAdd">Comunicado a adicionar (id_sala, titulo, corpo, etc.)</param>
        /// <returns>Response com Status Code e mensagem (Status Code 200 caso sucesso, ou 500 INTERNAL SERVER ERROR caso tenha havido algum erro</returns>
        public static async Task<Response> AddComunicado(string conString, Comunicado comunicadoToAdd)
        {
            Response response = new Response();
            try
            {
                if (await ComunicadoService.AddComunicado(conString, comunicadoToAdd))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Comunicado was added to sala.";
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
        /// Trata da parte lógica relativa à atualização de um comunicado que resida na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (PATCH - Comunicado (UpdateComunicado))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="comunicadoToUpdate">Comunicado inserida pelo gestor para atualizar</param>
        /// <returns>Response com Status Code, mensagem e dados (Comunicado atualizado)</returns>
        public static async Task<Response> UpdateComunicado(string conString, Comunicado comunicadoToUpdate)
        {
            Response response = new Response();
            try
            {
                Comunicado comunicadoReturned = await ComunicadoService.UpdateComunicado(conString, comunicadoToUpdate);
                if(comunicadoReturned.IdComunicado == 0)
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Comunicado was not found.";
                }
                else
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Comunicado was updated.";
                    response.Data = comunicadoReturned;
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
