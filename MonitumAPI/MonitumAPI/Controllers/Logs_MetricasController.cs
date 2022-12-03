using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonitumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Logs_MetricasController : ControllerBase
    {
        // GET: api/<Logs_MetricasController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Logs_MetricasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Logs_MetricasController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Logs_MetricasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Logs_MetricasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
