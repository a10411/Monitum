using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitumAPI.Utils;
using MonitumBLL.Logic;
using MonitumBLL.Utils;
using MonitumDAL;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace MonitumAPI.Controllers
{
    /// <summary>
    /// Controller para a definição de rotas da API para o CRUD relativo ao Gestor
    /// Rota base = api/Gestor (api é localhost ou é um link, se estiver publicada)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GestorController : Controller
    {
        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>
        private readonly IConfiguration _configuration;
        public GestorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Request GET relativo aos Gestores
        /// Apenas um admin consegue fazer este request com sucesso (Authorize(Roles="Admin"))
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente, retornará a lista de Gestores</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllGestores()
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await GestorLogic.GetGestores(CS);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
            
        }

        /// <summary>
        /// Request POST relativo ao Login de Gestor
        /// </summary>
        /// <param name="email">Email do gestor</param>
        /// <param name="password">Password do gestor, para posteriormente, no DAL, fazer a confirmação do hash e salt</param>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente (caso o gestor tenha introduzido as credenciais corretas), retorna uma resposta de sucesso (gestor autenticado)</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> LoginGestor(string email, string password)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await GestorLogic.LoginGestor(CS, email, password);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            JwtUtils jwt = new JwtUtils(_configuration);
            var token = jwt.GenerateJWTToken("gestor");
            response.Data = token;
            return new JsonResult(response);
            //return new JsonResult(token);
        }

        /// <summary>
        /// Request POST relativo ao Registo de Gestor
        /// </summary>
        /// <param name="email">Email do gestor para registar</param>
        /// <param name="password">Password do gestor para registar, posteriomente, no DAL, fará a conversão para hash e salt</param>
        /// <returns>Retorna a response obtida pelo DLL para o utilizador. Idealmente (caso o gestor tenha introduzido dados válidos para registo), retonrará uma resposta de sucesso (gestor registado).</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> RegistoGestor(string email, string password)
        {
            // Confirmar se o email introduzido é válido
            if (!InputValidator.emailChecker(email)) return StatusCode((int)MonitumBLL.Utils.StatusCodes.BADREQUEST);

            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await GestorLogic.RegisterGestor(CS, email, password);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }
    }
}
