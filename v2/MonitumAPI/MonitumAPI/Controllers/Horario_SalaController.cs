using Microsoft.AspNetCore.Mvc;
using MonitumBLL.Logic;
using MonitumBLL.Utils;
using MonitumBOL.Models;
using MonitumDAL;

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

        [HttpPut]
        public async Task<IActionResult> UpdateHorarioSala(Horario_Sala horarioToUpdate)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await Horario_SalaLogic.UpdateHorario(CS, horarioToUpdate);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }
    }
}
