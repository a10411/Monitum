using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitumBLL.Logic;
using MonitumBLL.Utils;
using MonitumBOL.Models;
using MonitumDAL;
using Swashbuckle.AspNetCore.Annotations;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace MonitumAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetricaController : Controller
    {
        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>
        private readonly IConfiguration _configuration; 

        public MetricaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Request POST relativo a uma métrica, que o gestor pretende adicionar
        /// Apenas um gestor consegue fazer este request com sucesso (Authorize)
        /// </summary>
        /// <param name="metricaToAdd">Métrica a adicionar à base de dados</param>
        /// <returns>Retorna a response obtida pelo BLL para o gestor. Idealmente, retornará uma response que diz que o POST foi bem sucedido.</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddMetrica(Metrica metricaToAdd)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await MetricaLogic.AddMetrica(CS, metricaToAdd);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }



    }
}
