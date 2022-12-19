using Microsoft.AspNetCore.Mvc;
using MonitumBLL.Logic;
using MonitumBLL.Utils;
using MonitumBOL.Models;
using MonitumDAL;

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
    }
}
