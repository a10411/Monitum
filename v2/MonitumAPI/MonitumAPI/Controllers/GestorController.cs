using Microsoft.AspNetCore.Mvc;
using MonitumAPI.Models;
using System.Data;
using System.Data.SqlClient;

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
        public IActionResult Get()
        {
            List<Gestor> gestorList = new List<Gestor>();
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Gestor", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var gestor = new Gestor();

                    gestor.IdGestor = Convert.ToInt32(rdr["id_gestor"]);
                    gestor.Email = rdr["email"].ToString();
                    gestor.Password = rdr["password"].ToString();

                    gestorList.Add(gestor);


                }
            }
            return new JsonResult(gestorList);

            
        }
    }
}
