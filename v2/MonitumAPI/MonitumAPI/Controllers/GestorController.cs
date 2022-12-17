using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using MonitumBOL.Models;
using MonitumBLL.Logic;
using MonitumBLL.Utils;

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
        /// </summary>
        /// <returns>Retorna todos os registos de Gestores disponíveis na Base de Dados</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllGestores()
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await GestorLogic.GetGestores(CS);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(GestorLogic.GetGestores(CS));
            
        }
    }
}
