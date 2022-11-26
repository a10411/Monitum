using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitumAPI.Models;

namespace MonitumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstabelecimentoController : ControllerBase
    {
        private static List<Estabelecimento> estabelecimentos = new List<Estabelecimento>
        {
            new Estabelecimento
            {
                IdEstabelecimento = 1,
                Nome = "Biblioteca de Arentim",
                Morada = "Rua de Pecelar numero 6 Arentim Braga"
            }
        };

        #region GET METHODS





        #endregion

        #region POST METHODS

        [HttpPost]

       
        #endregion

        #region PUT METHODS
        #endregion

        #region DELETE METHODS

        #endregion
    }
}
