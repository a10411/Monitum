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
    public class Horario_SalaLogic
    {
        public static async Task<Response> UpdateHorario(string conString, Horario_Sala horarioToUpdate)
        {
            Response response = new Response();
            try
            {
                Horario_Sala horarioSalaReturned = await Horario_SalaService.UpdateHorario(conString, horarioToUpdate);
                if (horarioSalaReturned.IdHorario == 0)
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Horario was not found.";
                } else
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Horario was updated.";
                    response.Data = horarioSalaReturned;
                }
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = e.ToString();
            }
            return response;
        }

        public static async Task<Response> AddHorarioToSala(string conString, Horario_Sala horarioToAdd)
        {
            Response response = new Response();
            try
            {
                if (await Horario_SalaService.AddHorario(conString, horarioToAdd))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Horario was added to sala.";
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
