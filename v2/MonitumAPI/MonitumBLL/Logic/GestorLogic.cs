using Microsoft.AspNetCore.Mvc;
using MonitumBLL.Utils;
using MonitumBOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBLL.Logic
{
    /// <summary>
    /// Classe que visa a implementação da parte de Business Logic Layer relativa ao Gestor
    /// Estes métodos são consumidos pelo MonitumAPI (Layer API), e são responsáveis por abstrair a API de detalhes como o Business Object Layer e da obtenção dos dados no DAL
    /// </summary>
    public class GestorLogic
    {
        /// <summary>
        /// Trata da parte lógica relativa à obtenção de todos os Gestores presentes na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (GET - Gestor)
        /// </summary>
        /// <param name="conString">Connection String da base de dados</param>
        /// <returns>Response com Status Code, mensagem e dados</returns>
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
    }
}
