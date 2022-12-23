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
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo aos gestores
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    /// </summary>
    public class GestorLogic
    {
        /// <summary>
        /// Trata da parte lógica relativa à obtenção de todos os Gestores presentes na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (GET - Gestor)
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <returns>Response com Status Code, mensagem e dados (Gestores recebidos do DAL)</returns>
        public static async Task<Response> GetGestores(string conString)
        {
            Response response = new Response();
            List<Gestor> gestorList = await MonitumDAL.GestorService.GetAllGestores(conString);
            if (gestorList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = gestorList;
            }
            return response;
        }

        public static async Task<Response> LoginGestor(string conString, string email, string password)
        {
            Response response = new Response();
            try
            {
                Boolean respBool = await GestorService.LoginGestor(conString, email, password);
                if (respBool)
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Gestor autenticado.";
                }
                else
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Credenciais inválidas";
                }
                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = e.ToString();
            }
            return response;
        }

        public static async Task<Response> RegisterGestor(string conString, string email, string password)
        {
            Response response = new Response();
            try
            {
                Boolean respBool = await GestorService.RegisterGestor(conString, email, password);
                if (respBool)
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Gestor registado.";
                }
                else
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Não foi possível registar o gestor.";
                }
                return response;
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
