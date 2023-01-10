using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitumBLL.Logic;
using MonitumBLL.Utils;
using MonitumBOL.Models;
using Swashbuckle.AspNetCore.Annotations;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace MonitumAPI.Controllers
{
    /// <summary>
    /// Controller para a definição de rotas da API para o CRUD relativo aos Comunicados
    /// Rota base = api/Comunicados (api é localhost ou é um link, se estiver publicada)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ComunicadoController : Controller
    {
        
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>
        public ComunicadoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Request GET relativo aos Comunicados
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente, retornará a lista de Comunicados</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        public async Task<IActionResult> GetAllComunicados()
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ComunicadoLogic.GetComunicados(CS);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);

        }

        [HttpGet]
        [Route("/ComunicadosByIdSala/sala/{idSala}")]
        public async Task<IActionResult> GetComunicadosByIdSala(int idSala)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ComunicadoLogic.GetComunicadosByIdSala(CS, idSala);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

        /// <summary>
        /// Request POST relativo aos Comunicados
        /// Apenas um gestor consegue fazer este request com sucesso (Authorize)
        /// </summary>
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
        public async Task<IActionResult> AddComunicado(Comunicado comunicadoToAdd)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ComunicadoLogic.AddComunicado(CS, comunicadoToAdd);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);

        }

        /// <summary>
        /// Request PATCH relativo a um Comunicado, que o gestor pretenda atualizar
        /// </summary>
        /// <param name="comunicadoToUpdate">Comunicado que o gestor pretende atualizar na BD</param>
        /// <returns>Retorna a resposta obtida pelo BLL para o gestor. Idealmente, retornará Comunicado atualizado, com um status code 200 (sucesso).</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateComunicado(Comunicado comunicadoToUpdate)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ComunicadoLogic.UpdateComunicado(CS, comunicadoToUpdate);
            if(response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

        /// <summary>
        /// Request DELETE relativo a um horário de uma sala, que o gestor pretenda apagar
        /// Apenas um gestor consegue fazer este request com sucesso (Authorize)
        /// </summary>
        /// <param name="idComunicado">ID do horário a remover da base de dados</param>
        /// <returns>Retorna a response obtida pelo BLL para o gestor. Idealmente, retornará uma response que diz que o DELETE foi bem sucedido.</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [Authorize]
        [HttpDelete]
        public async Task <IActionResult> DeleteComunicado(int idComunicado)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ComunicadoLogic.DeleteComunicado(CS, idComunicado);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

    }
}
