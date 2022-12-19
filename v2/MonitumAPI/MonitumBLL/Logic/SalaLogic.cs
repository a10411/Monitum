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
    public class SalaLogic
    {
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
    }
}
