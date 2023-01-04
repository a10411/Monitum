using MonitumBLL.Utils;
using MonitumDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBLL.Logic
{
    /// <summary>
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo aos administradores
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    /// </summary>
    public class AdministradorLogic
    {
        /// <summary>
        /// Trata da parte lógica relativa ao Login do Administrador
        /// Gera uma resposta que será utilizada pela MonitumAPI para resopnder ao request do utilizador (POST - Administrador (LoginAdministrador))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="email">Email do administrador que pretende fazer login (passado por parâmetro no MonitumAPI, ao chamar esta função)</param>
        /// <param name="password">Password do administrador que pretende fazer login (passado por parâmetro no MonitumAPI, ao chamar esta função)</param>
        /// <returns>Response com Status Code e mensagem (Status Code de sucesso, not found caso não exista um administrador na BD com estes dados ou erro interno)</returns>
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

        /// <summary>
        /// Trata da parte lógica relativa ao Registo do Administrador
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="email">Email do administrador que pretende fazer registo (passado por parâmetro no MonitumAPI, ao chamar esta função)</param>
        /// <param name="password">Password do administrador que pretende fazer registo (passado por parâmetro no MonitumAPI, ao chamar esta função)</param>
        /// <returns>Response com Status Code e mensagem (Status Code de sucesso ou erro interno)</returns>
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
                    response.Message = "Não foi possível registar o administrador.";
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
