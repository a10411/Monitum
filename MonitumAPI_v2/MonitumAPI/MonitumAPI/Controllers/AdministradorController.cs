using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitumAPI.Utils;
using MonitumBLL.Logic;
using MonitumBLL.Utils;
using Swashbuckle.AspNetCore.Annotations;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace MonitumAPI.Controllers
{
    /// <summary>
    /// Controller para a definição de rotas da API para o CRUD relativo ao Administrador
    /// Rota base = api/Administrador (api é localhost ou é um link, se estiver publicada)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AdministradorController : Controller
    {
        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>
        private readonly IConfiguration _configuration;
        public AdministradorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Request POST relativo ao Login de Administrador
        /// </summary>
        /// <param name="email">Email do administrador</param>
        /// <param name="password">Password do administrador, para posteriormente, no DAL, fazer a confirmação com hash e salt</param>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente (caso o administrador tenha introduzido as credenciais corretas), retorna uma resposta de sucesso (administrador autenticado)</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpPost]
        [Route("/LoginAdmin")]
        public async Task<IActionResult> LoginAdministrador(string email, string password)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await AdministradorLogic.LoginAdministrador(CS, email, password);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            JwtUtils jwt = new JwtUtils(_configuration);
            var token = jwt.GenerateJWTToken("Admin");
            response.Data = token;
            return new JsonResult(response);
            //return new JsonResult(token);
        }

        /// <summary>
        /// Request POST relativo ao Registo de Administrador
        /// </summary>
        /// <param name="email">Email do administrador para registar</param>
        /// <param name="password">Password do administrador para registar, posteriormente, no DAL, fará a conversão para hash e salt</param>
        /// <returns>Retorna a response obtida pelo DLL para o utilizador. Idealmente (caso o administrador tenha introduzido dados válidos para registo), retornará uma resposta de sucesso (administrador registado).</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        //[Authorize(Roles = "Admin")] apenas para efeitos de teste!
        [HttpPost]
        [Route("/RegisterAdmin")]
        public async Task<IActionResult> RegistoAdministrador(string email, string password)
        {
            // Confirmar se o email introduzido é válido
            if (!InputValidator.emailChecker(email)) return StatusCode((int)MonitumBLL.Utils.StatusCodes.BADREQUEST);

            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await AdministradorLogic.RegisterAdministrador(CS, email, password);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }
    }
}
