using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonitumAPI.Controllers;
using MonitumBOL.Models;

namespace MonitumAPI.Tests.Controllers
{
    /// <summary>
    /// Classe que visa testar todas as funções do comunicado controller
    /// </summary>
    public class ComunicadoControllerTests
    {

        /// <summary>
        /// Obtenção da config (IConfiguration) presente no app.settings.test.json deste projeto
        /// </summary>
        /// <returns>config</returns>
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                 .AddEnvironmentVariables()
                 .Build();
            return config;
        }

        /// <summary>
        /// Teste ao GetAllComunicados
        /// Resposta dada pela função deve ser um status code 204 ou um objeto Response com status code Success ou NoContent
        /// </summary>
        [Fact]
        public async void ComunicadoController_GetAllComunicados_ReturnOK()
        {
            // Arrange
            var config = InitConfiguration();
            var controller = new ComunicadoController(config);

            // Act
            IActionResult result = await controller.GetAllComunicados();


            // Assert
            result.Should().NotBeNull();

            // Status Code foi retornado em vez de response
            // Apenas status code de no content (204 é aceite como válido!)
            var statusCode = result as StatusCodeResult;
            if (statusCode != null)
            {
                statusCode.StatusCode.Should().Be(204);
            }
            
            // Foi retornada uma response!
            // Apenas uma response com status code Success ou No Content é aceite
            var jsonResult = result as JsonResult;
            if (jsonResult != null)
            {
                var response = jsonResult.Value as MonitumBLL.Utils.Response;
                response.Should().NotBeNull();
                response.StatusCode.Should().BeOneOf(
                    MonitumBLL.Utils.StatusCodes.SUCCESS,
                    MonitumBLL.Utils.StatusCodes.NOCONTENT
                    );
            }
        }

        /// <summary>
        /// Teste ao AddComunicado
        /// Deve responder com Status Code Success que indica que o comunicado foi adicionado
        /// </summary>
        [Fact]
        public async void ComunicadoController_AddComunicado_ReturnOK()
        {
            
            // Arrange
            var config = InitConfiguration();
            var controller = new ComunicadoController(config);
            // Comunicado válido
            var comunicado = new Comunicado(0, 4, "Test comunicado!", "Test corpo", new DateTime(2023, 01, 03, 02, 40, 00));
            // Comunicado inválido
            // var comunicado = new Comunicado(0, 99, "Test comunicado!", "Test corpo", new DateTime(2023, 01, 03, 02, 40, 00));

            // Act
            IActionResult result = await controller.AddComunicado(comunicado);

            // Assert
            result.Should().NotBeNull();

            var jsonResult = result as JsonResult;
            jsonResult.Should().NotBeNull();

            var response = jsonResult.Value as MonitumBLL.Utils.Response;
            response.Should().NotBeNull();
            response.StatusCode.Should().BeOneOf(
                MonitumBLL.Utils.StatusCodes.SUCCESS,
                MonitumBLL.Utils.StatusCodes.NOCONTENT
                );
            

        }

        /// <summary>
        /// Teste ao UpdateComunicado
        /// Deve responder com Status Code Success que indica que o comunicado foi atualizado
        /// </summary>
        [Fact]
        public async void ComunicadoController_UpdateComunicado_ReturnOK()
        {
            // Arrange
            var config = InitConfiguration();
            var controller = new ComunicadoController(config);
            // Comunicado válido
            var comunicado = new Comunicado(1, 0, "Test comunicado updated!!", "Test corpo updated", new DateTime(2023, 01, 03, 02, 40, 00));
            // Comunicado inválido
            // var comunicado = new Comunicado(99, 0, "Test comunicado!", "Test corpo", new DateTime(2023, 01, 03, 02, 40, 00));

            // Act
            IActionResult result = await controller.UpdateComunicado(comunicado);

            // Assert
            result.Should().NotBeNull();

            var jsonResult = result as JsonResult;
            jsonResult.Should().NotBeNull();

            var response = jsonResult.Value as MonitumBLL.Utils.Response;
            response.Should().NotBeNull();
            response.StatusCode.Should().BeOneOf(
                MonitumBLL.Utils.StatusCodes.SUCCESS,
                MonitumBLL.Utils.StatusCodes.NOCONTENT
                );

        }

        /// <summary>
        /// Teste ao DeleteComunicado
        /// Deve responder com Status Code Success que indica que o comunicado foi apagado
        /// </summary>
        [Fact]
        public async void ComunicadoController_DeleteComunicado_ReturnOK()
        {
            // Arrange
            var config = InitConfiguration();
            var controller = new ComunicadoController(config);
            // Comunicado válido
            int idComunicado = 1;
            // Comunicado inválido
            // int idComunicado = 999;

            // Act
            IActionResult result = await controller.DeleteComunicado(idComunicado);

            // Assert
            result.Should().NotBeNull();

            var jsonResult = result as JsonResult;
            jsonResult.Should().NotBeNull();

            var response = jsonResult.Value as MonitumBLL.Utils.Response;
            response.Should().NotBeNull();
            response.StatusCode.Should().BeOneOf(
                MonitumBLL.Utils.StatusCodes.SUCCESS,
                MonitumBLL.Utils.StatusCodes.NOCONTENT
                );
        }
        
    }

    
}
