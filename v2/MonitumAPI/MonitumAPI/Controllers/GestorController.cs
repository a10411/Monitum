using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using MonitumBOL.Models;

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
            // pass to BLL! 
            List<Gestor> gestorList = await MonitumDAL.GestorService.GetAllGestores(); 
            return new JsonResult(gestorList);

            
        }
    }
}
