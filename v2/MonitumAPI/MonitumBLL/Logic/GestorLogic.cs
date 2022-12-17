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
    public class GestorLogic
    {
        public static async Task<Response> GetGestores(string conString)
        {
            Response response = new Response();
            List<Gestor> gestorList = await MonitumDAL.GestorService.GetAllGestores(conString);
            if (gestorList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = new JsonResult(gestorList);
            }
            return response;
        }
    }
}
