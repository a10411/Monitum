using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using MonitumBOL.Models;
using MonitumBLL.Logic;

namespace MonitumAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GestorController : Controller
    {
        private readonly IConfiguration _configuration;

        public GestorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }   

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            return new JsonResult(GestorLogic.GetGestores(CS));
        }
    }
}
