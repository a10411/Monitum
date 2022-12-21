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
    public class SalaController : Controller
    {
        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>
        private readonly IConfiguration _configuration;
        public SalaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Request POST relativo às Salas (adicionar uma sala a um estabelecimento)
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para a sala. Idealmente, retornará uma resposta com status code 200 (sucesso)</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpPost]
        public async Task<IActionResult> AddSalaToEstabelecimento(Sala salaToAdd)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await SalaLogic.AddSalaToEstabelecimento(CS, salaToAdd);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);


        }

        [HttpGet]
        [Route("/estabelecimento/{idEstabelecimento}")]
        public async Task<IActionResult> GetSalaByEstabelecimento(int idEstabelecimento)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await SalaLogic.GetSalas(CS, idEstabelecimento);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

        [HttpGet]
        [Route("/GetLastLogMetricaSala/sala/{idSala}/metrica/{idMetrica}")]
        public async Task<IActionResult>  GetMetricaBySala(int idSala, int idMetrica)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await SalaLogic.GetMetricaBySala(CS, idMetrica, idSala);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);    
            }
            return new JsonResult(response);
        }
    }
}
