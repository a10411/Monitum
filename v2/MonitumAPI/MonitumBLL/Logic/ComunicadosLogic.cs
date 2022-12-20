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
    /// 
    /// </summary>
    public class ComunicadosLogic
    {
        /// <summary>
        /// Trata da parte lógica relativa à obtenção de todos os comunicados na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (POST - Comunicado)
        /// </summary>
        /// <param name="conString">Connection String da base de dados</param>
      
        /// <returns>Response com Status Code e mensagem (Status Code 200 caso sucesso, ou 500 INTERNAL SERVER ERROR caso tenha havido algum erro</returns>
        public static async Task<Response> GetComunicados(string conString)
        {
            Response response = new Response();
            List<Comunicados> gestorList = await ComunicadosService.GetAllComunicados(conString);
            if (gestorList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = new JsonResult(gestorList);
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à criação de um comunicado na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (POST - Comunicado)
        /// </summary>
        /// <param name="conString">Connection String da base de dados</param>
        /// <param name="comunicadoToAdd">Parâmetros do comunicado a adicionar (idEstabelecimento, idEstado)</param>
        /// <returns>Response com Status Code e mensagem (Status Code 200 caso sucesso, ou 500 INTERNAL SERVER ERROR caso tenha havido algum erro</returns>
        public static async Task<Response> AddComunicado(string conString, Comunicados comunicadoToAdd)
        {
            Response response = new Response();
            try
            {
                if (await ComunicadosService.AddComunicado(conString, comunicadoToAdd))
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
    }
}
