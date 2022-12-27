using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitumAPI.Utils;
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
    public class Horario_SalaController : Controller
    {
        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>
        private readonly IConfiguration _configuration;
        public Horario_SalaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Request GET relativo aos horários de uma sala, que o utilizador pretenda visualizar
        /// </summary>
        /// <param name="idSala">ID da Sala para a qual o utilizador pretende ver os horários</param>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmnete, retornará a lista de horários, com uma mensagem de sucesso.</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        public async Task<IActionResult> GetHorariosSalaByIdSala(int idSala)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await Horario_SalaLogic.GetHorariosByIdSala(CS, idSala);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

        /// <summary>
        /// Request PUT relativo a um horário de uma sala, que o gestor pretenda substituir
        /// Apenas um gestor consegue fazer este request com sucesso (Authorize)
        /// </summary>
        /// <param name="horarioToUpdate">Horário que visa substituir o que reside na base de dados</param>
        /// <returns>Retorna a response obtida pelo BLL para o gestor. Idealmente, retornará o novo horário, com uma mensagem de sucesso.</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutHorarioSala(Horario_Sala horarioToUpdate)
        {
            // Confirmar se dia da semana é válido
            if (!InputValidator.weekdayPTChecker(horarioToUpdate.DiaSemana)) return StatusCode((int)MonitumBLL.Utils.StatusCodes.BADREQUEST);

            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await Horario_SalaLogic.PutHorario(CS, horarioToUpdate);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

        /// <summary>
        /// Request UPDATE relativo a um horário de uma sala, que o gestor pretenda atualizar
        /// Apenas um gestor consegue fazer este request com sucesso (Authorize)
        /// </summary>
        /// <param name="horarioToUpdate">Horário que visa atualizar o que reside na base de dados</param>
        /// <returns>Retorna a response obtida pelo BLL para o gestor. Idealmente, retornará o novo horário, com uma mensagem de sucesso.</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateHorarioSala(Horario_Sala horarioToUpdate)
        {
            if (horarioToUpdate.DiaSemana != null && horarioToUpdate.DiaSemana != String.Empty)
            {
                // Confirmar se dia da semana é válido
                if (!InputValidator.weekdayPTChecker(horarioToUpdate.DiaSemana)) return StatusCode((int)MonitumBLL.Utils.StatusCodes.BADREQUEST);
            }
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await Horario_SalaLogic.UpdateHorario(CS, horarioToUpdate);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }


        /// <summary>
        /// Request POST relativo a um horário de uma sala, que o gestor pretenda adicionar
        /// Apenas um gestor consegue fazer este request com sucesso (Authorize) 
        /// </summary>
        /// <param name="horarioToAdd">Horário a adicionar à base de dados</param>
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
        public async Task<IActionResult> AddHorarioSala(Horario_Sala horarioToAdd)
        {
            // Confirmar se dia da semana é válido
            if (!InputValidator.weekdayPTChecker(horarioToAdd.DiaSemana)) return StatusCode((int)MonitumBLL.Utils.StatusCodes.BADREQUEST);

            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await Horario_SalaLogic.AddHorarioToSala(CS, horarioToAdd);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }


        /// <summary>
        /// Request DELETE relativo a um horário de uma sala, que o gestor pretenda apagar
        /// Apenas um gestor consegue fazer este request com sucesso (Authorize)
        /// </summary>
        /// <param name="IdHorario">ID do horário a remover da base de dados</param>
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
        public async Task<IActionResult> DeleteHorarioSala(int IdHorario)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await Horario_SalaLogic.DeleteHorarioSala(CS, IdHorario);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }
    }
}
