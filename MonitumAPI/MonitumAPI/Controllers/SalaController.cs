using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonitumAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonitumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaController : ControllerBase
    {

        #region GET METHODS


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Dados de todas as Salas</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using (var context = new MonitumDBContext())
                {
                    return new JsonResult(context.Salas.ToList());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getsalasbyestabelecimento/{idEstabelecimento}")]
        [HttpGet]
        public IActionResult GetSalasByEstabelecimento(int idEstabelecimento)
        {
            using (var context = new MonitumDBContext())
            {
                try
                {

                    Estabelecimento est = context.Estabelecimentos.Where(e =>
                    e.IdEstabelecimento == idEstabelecimento).FirstOrDefault();
                    if (est == null) BadRequest();

                    Sala sal = context.Salas.Where(s =>
                    s.IdEstabelecimento == est.IdEstabelecimento).FirstOrDefault();

                    return new JsonResult(sal);


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
                
           
        }

        [HttpGet]

        public IActionResult GetMetricaById(int idMetrica) 
        {

            using(var context = new MonitumDBContext())
            {
                try
                {
                    List<Sala> salas = context.Salas.ToList();
                    List<LogsMetrica> logsMetricas = context.LogsMetricas.ToList();

                    var x = salas.Join(logsMetricas, s => s.IdSala, lm => lm.IdSala, (s, lm) => new
                    {
                        s.IdEstabelecimento,
                        s.IdEstado,
                        lm.IdLog,
                        lm.IdMetrica,
                        lm.ValorMetrica,
                        lm.DataHora

                    }).ToList();

                    return new JsonResult(x.Where(x=> x.IdMetrica == idMetrica).ToList());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
                
        }

        #endregion

        #region POST METHODS

        // POST api/<SalaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        #endregion

        #region PUT METHODS
        // PUT api/<SalaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        #endregion

        #region DELETE METHODS
        // DELETE api/<SalaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion
    }
}
