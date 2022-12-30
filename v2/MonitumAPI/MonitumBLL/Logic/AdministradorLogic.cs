using MonitumBLL.Utils;
using MonitumDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBLL.Logic
{
    public class AdministradorLogic
    {
        public static async Task<Response> LoginAdministrador(string conString, string email, string password)
        {
            Response response = new Response();
            try
            {
                Boolean respBool = await AdministradorService.LoginAdministrador(conString, email, password);
                if (respBool)
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Administrador autenticado.";
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

        
        public static async Task<Response> RegisterAdministrador(string conString, string email, string password)
        {
            Response response = new Response();
            try
            {
                Boolean respBool = await AdministradorService.RegisterAdministrador(conString, email, password);
                if (respBool)
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Administrador registado.";
                }
                else
                {
                    response.StatusCode = StatusCodes.INTERNALSERVERERROR;
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
