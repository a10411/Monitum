using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using MonitumAPI.Controllers;
using MonitumBOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumAPI.Tests.Controllers
{
    public class ComunicadoControllerTests
    {
        // private readonly IConfiguration _configuration;
        public ComunicadoControllerTests()
        {
            //_configuration = A.Fake<IConfiguration>();
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                 .AddEnvironmentVariables()
                 .Build();
            return config;
        }

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

        [Fact]
        public async void ComunicadoController_AddComunicado_ReturnOK()
        {
            /*
            // Arrange
            var config = InitConfiguration();
            var controller = new ComunicadoController(config);
            var comunicado = new Comunicado(0, );

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
            */

        }
    }
}
