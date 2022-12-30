using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitumAPI.Utils;
using MonitumBLL.Logic;
using MonitumBLL.Utils;

namespace MonitumAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministradorController : Controller
    {
        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>
        private readonly IConfiguration _configuration;
        public AdministradorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/LoginAdmin")]
        public async Task<IActionResult> LoginAdministrador(string email, string password)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await AdministradorLogic.LoginAdministrador(CS, email, password);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            JwtUtils jwt = new JwtUtils(_configuration);
            var token = jwt.GenerateJWTToken("Admin");
            response.Data = token;
            return new JsonResult(response);
            //return new JsonResult(token);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("/RegisterAdmin")]
        public async Task<IActionResult> RegistoAdministrador(string email, string password)
        {
            // Confirmar se o email introduzido é válido
            if (!InputValidator.emailChecker(email)) return StatusCode((int)MonitumBLL.Utils.StatusCodes.BADREQUEST);

            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await AdministradorLogic.RegisterAdministrador(CS, email, password);
            if (response.StatusCode != MonitumBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }
    }
}
