using MonitumBLL.Utils;
using MonitumBOL.Models;
using MonitumDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBLL.Logic
{
    /// <summary>
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo aos horários das salas
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    /// </summary>
    public class Horario_SalaLogic
    {
        /// <summary>
        /// Trata da parte lógica relativa à substituição (pedido PUT) de um horário que resida na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do gestor (PUT - Horario (PutHorarioSala))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="horarioToUpdate">Horário inserido pelo gestor para substituir</param>
        /// <returns>Response com Status Code, mensagem e dados (Horario atualizado)</returns>
        public static async Task<Response> PutHorario(string conString, Horario_Sala horarioToUpdate)
        {
            Response response = new Response();
            try
            {
                Horario_Sala horarioSalaReturned = await Horario_SalaService.PutHorario(conString, horarioToUpdate);
                if (horarioSalaReturned.IdHorario == 0)
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Horario was not found or there is overlapping of schedules.";
                } else
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Horario was updated.";
                    response.Data = horarioSalaReturned;
                }
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = e.ToString();
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à atualização (pedido UPDATE) de um horário que resida na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do gestor (UPDATE - Horario (UpdateHorarioSala))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="horarioToUpdate">Horário inserido pelo gestor para atualizar</param>
        /// <returns></returns>
        public static async Task<Response> UpdateHorario(string conString, Horario_Sala horarioToUpdate)
        {
            Response response = new Response();
            try
            {
                Horario_Sala horarioSalaReturned = await Horario_SalaService.UpdateHorario(conString, horarioToUpdate);
                if (horarioSalaReturned.IdHorario == 0)
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Horario was not found or there is overlapping of schedules.";
                }
                else
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Horario was updated.";
                    response.Data = horarioSalaReturned;
                }
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = e.ToString();
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à obtenção dos horários de uma sala, dados estes que residem na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (GET - Horario (GetHorariosSalaByIdSala))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="idSala">ID da Sala para a qual queremos visualizar horários</param>
        /// <returns>Response com Status Code, mensagem e dados (Horários da sala)</returns>
        public static async Task<Response> GetHorariosByIdSala(string conString, int idSala)
        {
            Response response = new Response();
            try
            {   
                List<Horario_Sala> horarioSalaList =  await Horario_SalaService.GetHorariosSalaByIdSala(conString, idSala);    
                if(horarioSalaList.Count != 0)
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Sucesso na obtenção dos dados";
                    response.Data = horarioSalaList;
                }
            }
            catch (Exception e)
            {
                throw;
            }
               return response;

        }

        /// <summary>
        /// Trata da parte lógica relativa à inserção de um horário relativo a uma sala na base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (POST - Horario (AddHorarioSala))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="horarioToAdd">Horário inserido pelo gestor para adicionar à base de dados</param>
        /// <returns>Response com Status Code e mensagem (indicando que o horário foi adicionado)</returns>
        public static async Task<Response> AddHorarioToSala(string conString, Horario_Sala horarioToAdd)
        {
            Response response = new Response();
            try
            {
                if (await Horario_SalaService.AddHorario(conString, horarioToAdd))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Horario was added to sala.";
                }
                else
                {
                    response.StatusCode = StatusCodes.BADREQUEST;
                    response.Message = "There is overlapping of schedules.";
                }
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = e.ToString();
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à remoção de um horário relativo a uma sala da base de dados
        /// Gera uma resposta que será utilizada pela MonitumAPI para responder ao request do utilizador (DELETE - Horario (DeleteHorarioSala))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="IdHorario">ID do horário a remover</param>
        /// <returns>Response com Status Code e mensagem (indicando que o horário foi adicionado)</returns>
        public static async Task<Response> DeleteHorarioSala(string conString, int IdHorario)
        {
            Response response = new Response();
            try
            {
                if (await Horario_SalaService.DeleteHorario(conString, IdHorario))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Horario was deleted from sala.";
                }
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = e.ToString();
            }
            return response;
        }
    }
}
