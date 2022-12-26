using Microsoft.AspNetCore.Mvc;
using MonitumBLL.Logic;
using MonitumBLL.Utils;
using MonitumBOL.Models;
using Swashbuckle.AspNetCore.Annotations;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace MonitumAPI.Controllers
{
    /// <summary>
    /// Controller para a definição de rotas da API para o CRUD relativo a Log Metrica
    /// Rota base = api/Log_Metrica (api é localhost ou é um link, se estiver publicada)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Log_MetricaController : Controller
    {
        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>
        private readonly IConfiguration _configuration;
        public Log_MetricaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Request GET relativo a log metrica
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente, retornará a lista de Logs</returns>
        [SwaggerResponse(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        public async Task<IActionResult> GetAllLogMetricas(int idSala)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await Log_MetricaLogic.GetAllLogMetrica(CS, idSala);
            if(response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }
        [SwaggerResponse(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpPost]
        public async Task<IActionResult> AddLogMetrica(Log_Metrica logMetricaToAdd)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await Log_MetricaLogic.AddLogMetrica(CS, logMetricaToAdd);
            if(response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

    }
}
