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
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente, retornará uma resposta com status code 200 (sucesso)</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [Authorize]
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

        /// <summary>
        /// Request GET relativo à obtenção das salas de um estabelecimento
        /// </summary>
        /// <param name="idEstabelecimento">ID do estabelecimento para o qual o utilizador pretende ver as salas existentes</param>
        /// <returns>Retorna a resposta obtida pelo BLL para o utilizador. Idealmente, retornará uma lista de salas, com um status code 200 (sucesso).</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        [Route("/estabelecimento/{idEstabelecimento}")]
        public async Task<IActionResult> GetSalasByEstabelecimento(int idEstabelecimento)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await SalaLogic.GetSalas(CS, idEstabelecimento);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

        /// <summary>
        /// Request GET relativo à obtenção da última log de uma determinada métrica de uma sala
        /// Será útil para expor o ruído/ocupação "atual" na aplicação ("atual" dado que o objetivo é que o Arduino envie logs de 5 em 5 minutos, por isso, a última será a "atual")
        /// </summary>
        /// <param name="idSala">ID da sala para a qual queremos visualizar a última métrica</param>
        /// <param name="idMetrica">ID da métrica que queremos visualizar (ruído tem um determinado ID, ocupação tem outro, etc.)</param>
        /// <returns>Retorna a resposta obtida pelo BLL para o utilizador. Idealmente, retornará a log pretendida, com um status code 200 (sucesso).</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        [Route("/GetLastLogMetricaSala/sala/{idSala}/metrica/{idMetrica}")]
        public async Task<IActionResult> GetLastMetricaBySala(int idSala, int idMetrica)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await SalaLogic.GetLastMetricaBySala(CS, idMetrica, idSala);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);    
            }
            return new JsonResult(response);
        }

        /// <summary>
        /// Request PATCH relativo à atualização do estado de uma sala
        /// Útil para quando o gestor pretender arquivar uma sala ou voltar a colocar uma sala ativa
        /// Apenas pode ser feito pelo Gestor
        /// </summary>
        /// <param name="idSala">ID sala a atualizar</param>
        /// <param name="idEstado">ID estado para o qual pretendemos atualizar a sala</param>
        /// <returns>Retorna a resposta obtida pelo BLL para o gestor. Idealmente, retornará a sala atualizada, com um status code 200 (sucesso).</returns>
        [Authorize]
        [HttpPatch]
        [Route("/UpdateEstadoSala/sala/{idSala}/estado/{idEstado}")]
        public async Task<IActionResult> UpdateEstadoSala(int idSala, int idEstado)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await SalaLogic.UpdateEstadoSala(CS, idSala, idEstado);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);

        }


        /// <summary>
        /// Request PUT relativo a uma sala, que o gestor pretenda atualizar
        /// </summary>
        /// <param name="idSala"></param>
        /// <param name="idEstabelecimento"></param>
        /// <param name="idEstado"></param>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [Authorize]
        [HttpPatch]
        [Route("/UpdateSala/sala/{idSala}")]
        public async Task<IActionResult> UpdateSala(int idSala, int idEstabelecimento, int idEstado)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await SalaLogic.UpdateSala(CS, idSala, idEstabelecimento, idEstado);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }
    }
}
