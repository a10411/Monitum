using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitumBOL.Models;
using MonitumDAL.Utils;

namespace MonitumDAL
{
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes à Sala
    /// Isto é, todos os acessos à base de dados relativos à sala estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class SalaService
    {
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e adicionar um registo de uma sala (adicionar uma sala relativa a um estabelecimento)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <returns>True caso tenha adicionado ou retorna a exceção para a camada lógica caso tenha havido algum erro</returns>
        public static async Task<Boolean> AddSala(string conString, Sala salaToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addSala = "INSERT INTO Sala (id_estabelecimento, id_estado, nome) VALUES (@idEstabelecimento, @idEstado, @nome)";
                    using (SqlCommand queryAddSala = new SqlCommand(addSala))
                    {
                        queryAddSala.Connection = con;
                        queryAddSala.Parameters.Add("@idEstabelecimento", SqlDbType.Int).Value = salaToAdd.IdEstabelecimento;
                        queryAddSala.Parameters.Add("@idEstado", SqlDbType.Int).Value = salaToAdd.IdEstado;
                        queryAddSala.Parameters.Add("@nome", SqlDbType.Char).Value = salaToAdd.Nome;
                        con.Open();
                        queryAddSala.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            } catch (Exception e){
                throw;
            }
            
            
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter as salas de um estabelecimento
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="idEstabelecimento">ID do estabelecimento para o qual pretendemos ver as salas</param>
        /// <returns>Lista de salas do estabelecimento</returns>
        public static async Task<List<Sala>> GetSalasByEstabelecimento(string conString, int idEstabelecimento)
        {
            try
            {   
                var salaList = new List<Sala>();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM Sala WHERE id_estabelecimento = {idEstabelecimento}",con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Sala sala = new Sala(rdr);
                        salaList.Add(sala);
                    }
                    rdr.Close();
                    con.Close();
                }
                return salaList;
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public static async Task<Sala> GetSalaByIdSala(string conString, int idSala)
        {
            try
            {
                var sala = new Sala();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM Sala WHERE id_sala = {idSala}", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        sala = new Sala(rdr);
                    }
                    rdr.Close();
                    con.Close();
                }
                return sala;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter a última métrica relativa a uma sala (métrica "atual")
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="idMetrica">ID da métrica que o utilizador pretende visualizar (ruído, ocupação, etc.)</param>
        /// <param name="idSala">ID da sala para a qual o utilizador pretende visualizar a métrica</param>
        /// <returns>Último log da métrica pretendida</returns>
        public static async Task<Log_Metrica> GetLastMetricaBySala(string conString, int idMetrica, int idSala)
        {
            try
            {
                var log = new Log_Metrica();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT TOP 1 * FROM Log_Metrica WHERE id_sala = {idSala} AND id_metrica = {idMetrica} ORDER BY id_log DESC", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        log = new Log_Metrica(rdr);
                    }
                    rdr.Close();
                    con.Close();
                }
                return log;
            }
            catch (Exception e) 
            {
                throw;
            }
            
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter uma sala com um determinado ID
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="idSala">ID da sala que pretendemos obter</param>
        /// <returns>Sala pretendida, ou sala com id = 0 caso nenhuma tenha sido encontrada</returns>
        public static async Task<Sala> GetSala(string conString, int idSala)
        {
            Sala sala = new Sala();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Sala where id_sala = {idSala}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    sala = new Sala(rdr);
                }
                rdr.Close();
                con.Close();
            }

            return sala;
            // retorna uma sala com id = 0 caso não encontre nenhum com este ID
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e atualizar o estado de uma sala
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="idSala">ID da sala que pretendemos atualizar</param>
        /// <param name="idEstado">ID do estado para o qual queremos atualizar a sala</param>
        /// <returns>Sala atualizada</returns>
        public static async Task<Sala> UpdateEstadoSala(string conString, int idSala, int idEstado)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addSala = "UPDATE Sala set id_estado = @idEstado where id_sala = @idSala";
                    using (SqlCommand queryAddSala = new SqlCommand(addSala))
                    {
                        queryAddSala.Connection = con;
                        queryAddSala.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        queryAddSala.Parameters.Add("@idSala", SqlDbType.Int).Value = idSala;
                        con.Open();
                        queryAddSala.ExecuteNonQuery();
                        con.Close();
                        return await GetSala(conString, idSala);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa chamar métodos já existentes (reutiliza métodos) e indica se uma sala está aberta ou fechada atualmente
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="idSala">ID da sala que pretendemos obter esta informação</param>
        /// <returns>True se está aberta, false se está fechada</returns>
        public static async Task<Boolean> CheckSalaOpen(string conString, int idSala)
        {
            try
            {
                // checkar estado da sala, se for = 1 (Ativa) segue
                // checkar horario, se está aberta atualmente, true
                Sala salaRequested = await GetSala(conString, idSala);
                if (salaRequested.IdEstado == 1)
                {
                    List<Horario_Sala> horariosSalaObtained = await Horario_SalaService.GetHorariosSalaByIdSala(conString, idSala);
                    DayOfWeek todayWeekDay = DateTime.Today.DayOfWeek;
                    TimeSpan timeNow = DateTime.Now.TimeOfDay;
                    string todayWeekDayPT = WeekdayConvertion.WeekdayConverterEngToPT(todayWeekDay.ToString());
                    foreach (Horario_Sala horario_aux in horariosSalaObtained)
                    {
                        if (horario_aux.DiaSemana == todayWeekDayPT)
                        {
                            TimeSpan horaEntrada = horario_aux.HoraEntrada.TimeOfDay;
                            TimeSpan horaSaida = horario_aux.HoraSaida.TimeOfDay;
                            if (horaEntrada <= timeNow && horaSaida >= timeNow)
                            {
                                return true;
                            }
                        }
                    }

                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e substituir um registo de uma sala (substituir uma sala relativa a um estabelecimento)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="salaToUpdate">Sala a substituir</param>
        /// <returns>Sala nova (substituida) ou erro</returns>
        public static async Task<Sala> PutSala(string conString, Sala salaToUpdate)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addSala = "UPDATE Sala SET id_estabelecimento = @idEstabelecimento, id_estado = @idEstado, nome = @nome where id_sala = @idSala";
                    using (SqlCommand queryAddSala = new SqlCommand(addSala))
                    {
                        queryAddSala.Connection = con;
                        queryAddSala.Parameters.Add("@idSala", SqlDbType.Int).Value = salaToUpdate.IdSala;
                        queryAddSala.Parameters.Add("@idEstabelecimento", SqlDbType.Int).Value = salaToUpdate.IdEstabelecimento;
                        queryAddSala.Parameters.Add("@idEstado", SqlDbType.Int).Value = salaToUpdate.IdEstado;
                        queryAddSala.Parameters.Add("@nome", SqlDbType.Char).Value = salaToUpdate.Nome;
                        con.Open();
                        queryAddSala.ExecuteNonQuery();
                        con.Close();
                        return await GetSala(conString, salaToUpdate.IdSala);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e atualizar o registo de uma sala (atualizar uma sala relativa a um estabelecimento)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="salaUpdated">Sala a atualizar</param>
        /// <returns>Sala atualizada ou erro</returns>
        public static async Task<Sala> UpdateSala(string conString, Sala salaUpdated)
        {
            Sala salaAtual = await GetSala(conString, salaUpdated.IdSala);
            salaUpdated.IdSala = salaUpdated.IdSala != 0 ? salaUpdated.IdSala : salaAtual.IdSala;
            salaUpdated.IdEstabelecimento = salaUpdated.IdEstabelecimento != 0 ? salaUpdated.IdEstabelecimento : salaAtual.IdEstabelecimento;
            salaUpdated.IdEstado = salaUpdated.IdEstado != 0 ? salaUpdated.IdEstado : salaAtual.IdEstado;
            salaUpdated.Nome = salaUpdated.Nome != String.Empty && salaUpdated.Nome != null ? salaUpdated.Nome : salaAtual.Nome;

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string updateSala = "UPDATE Sala SET id_estabelecimento = @idEstabelecimento, id_estado = @idEstado, nome = @nome where id_sala = @idSala";
                    using (SqlCommand queryUpdateSala = new SqlCommand(updateSala))
                    {
                        queryUpdateSala.Connection = con;
                        queryUpdateSala.Parameters.Add("@idEstabelecimento", SqlDbType.Int).Value = salaUpdated.IdEstabelecimento;
                        queryUpdateSala.Parameters.Add("@idEstado", SqlDbType.Int).Value = salaUpdated.IdEstado;
                        queryUpdateSala.Parameters.Add("@nome", SqlDbType.Char).Value = salaUpdated.Nome;
                        queryUpdateSala.Parameters.Add("@idSala", SqlDbType.Int).Value = salaUpdated.IdSala;
                        con.Open();
                        queryUpdateSala.ExecuteNonQuery();
                        con.Close();
                        return await GetSala(conString, salaUpdated.IdSala);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }

    

}



