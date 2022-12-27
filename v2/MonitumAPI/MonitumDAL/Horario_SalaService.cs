using MonitumBOL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumDAL
{
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes ao Horario_Sala (Horário da Sala)
    /// Isto é, todos os acessos à base de dados relativos ao Horario_Sala estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class Horario_SalaService
    {
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter um horário de uma sala
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="idHorario">ID do horário que pretendemos obter</param>
        /// <returns>Horário obtido, caso não encontre nenhum, retorna um horário com ID = 0</returns>
        public static async Task<Horario_Sala> GetHorarioSala(string conString, int idHorario)
        {
            Horario_Sala horarioSala = new Horario_Sala();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Horario_Sala where id_horario = {idHorario}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    horarioSala = new Horario_Sala(rdr);
                }
                rdr.Close();
                con.Close();
            }

            return horarioSala;
            // retorna um horario com id = 0 caso não encontre nenhum com este ID
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter todos os horários relativos a uma sala
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="idSala">ID da sala para a qual pretendemos obter os horários</param>
        /// <returns>Horários da sala</returns>
        public static async Task<List<Horario_Sala>> GetHorariosSalaByIdSala(string conString, int idSala)
        {
            try
            {
                var horarioSalaList= new List<Horario_Sala>();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM Horario_Sala where id_sala = {idSala}", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while(rdr.Read())
                    {
                        Horario_Sala horarioSala = new Horario_Sala(rdr);
                        horarioSalaList.Add(horarioSala);
                        
                    }
                    rdr.Close();
                    con.Close();    
                }
                return horarioSalaList;
            }
            catch (Exception e) 
            {
                throw;
            }

        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e atualizar um horário lá existente
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="horarioUpdated">Horário atualizado</param>
        /// <returns>Horário atualizado (utilizando a função GetHorarioSala)</returns>
        public static async Task<Horario_Sala> UpdateHorario(string conString, Horario_Sala horarioUpdated)
        {
            if (await CheckSobreposicaoHorarios(conString, horarioUpdated) == false)
            {
                return new Horario_Sala();
                // horario com ID = 0
            }
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string updateHorario = "UPDATE Horario_Sala SET id_sala = @idSala, dia_semana = @diaSemana, hora_entrada = @horaEntrada, hora_saida = @horaSaida where id_horario = @idHorario";
                    using (SqlCommand queryUpdateHorario = new SqlCommand(updateHorario))
                    {
                        queryUpdateHorario.Connection = con;
                        queryUpdateHorario.Parameters.Add("@idSala", SqlDbType.Int).Value = horarioUpdated.IdSala;
                        queryUpdateHorario.Parameters.Add("@diaSemana", SqlDbType.VarChar).Value = horarioUpdated.DiaSemana;
                        queryUpdateHorario.Parameters.Add("@horaEntrada", SqlDbType.Time).Value = horarioUpdated.HoraEntrada.TimeOfDay;
                        queryUpdateHorario.Parameters.Add("@horaSaida", SqlDbType.Time).Value = horarioUpdated.HoraSaida.TimeOfDay;
                        queryUpdateHorario.Parameters.Add("@idHorario", SqlDbType.Int).Value = horarioUpdated.IdHorario;
                        con.Open();
                        queryUpdateHorario.ExecuteNonQuery();
                        con.Close();
                        return await GetHorarioSala(conString, horarioUpdated.IdHorario);
                    }
                }
            } catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e adicionar um horário a uma sala de um estabelecimento
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="horarioToAdd">Horário a adicionar à sala</param>
        /// <returns>True se tudo tenha corrido bem (horário adicionado), algum erro caso o horário não tenha sido adicionado.</returns>
        public static async Task<Boolean> AddHorario(string conString, Horario_Sala horarioToAdd)
        {
            if (await CheckSobreposicaoHorarios(conString, horarioToAdd) == false)
            {
                return false;
            }
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addHorario = "INSERT INTO Horario_Sala (id_sala,dia_semana,hora_entrada,hora_saida) VALUES (@idSala, @diaSemana, @horaEntrada, @horaSaida)";
                    using (SqlCommand queryAddHorario = new SqlCommand(addHorario))
                    {
                        queryAddHorario.Connection = con;
                        queryAddHorario.Parameters.Add("@idSala", SqlDbType.Int).Value = horarioToAdd.IdSala;
                        queryAddHorario.Parameters.Add("@diaSemana", SqlDbType.VarChar).Value = horarioToAdd.DiaSemana;
                        queryAddHorario.Parameters.Add("@horaEntrada", SqlDbType.Time).Value = horarioToAdd.HoraEntrada.TimeOfDay;
                        queryAddHorario.Parameters.Add("@horaSaida", SqlDbType.Time).Value = horarioToAdd.HoraSaida.TimeOfDay;
                        con.Open();
                        queryAddHorario.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            }
            catch
            {
                throw;
            }


        }

        public static async Task<Boolean> CheckSobreposicaoHorarios(string conString, Horario_Sala horarioToVerify)
        {
            try
            {
                List<Horario_Sala> horariosReceived = await GetHorariosSalaByIdSala(conString, horarioToVerify.IdSala);
                foreach (Horario_Sala horario_aux in horariosReceived)
                {
                    if (horario_aux.DiaSemana == horarioToVerify.DiaSemana)
                    {
                        if ((horarioToVerify.HoraEntrada >= horario_aux.HoraEntrada && horarioToVerify.HoraEntrada <= horario_aux.HoraSaida) || (horarioToVerify.HoraSaida >= horario_aux.HoraEntrada && horarioToVerify.HoraSaida <= horario_aux.HoraSaida) || (horario_aux.HoraEntrada >= horarioToVerify.HoraEntrada && horario_aux.HoraEntrada <= horarioToVerify.HoraSaida) || (horario_aux.HoraSaida >= horarioToVerify.HoraEntrada && horario_aux.HoraSaida <= horarioToVerify.HoraSaida))
                        {
                            return false;
                        }
                    }
                    
                }
                return true;
            } catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e apagar um horário existente na mesma
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="IdHorario">ID do horário a apagar</param>
        /// <returns>True caso tudo tenha corrido bem (horário removido), algum erro caso o horário não tenha sido removido.</returns>
        public static async Task<Boolean> DeleteHorario(string conString, int IdHorario)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string deleteHorario = $"DELETE FROM Horario_Sala where id_horario = {IdHorario}";
                    using (SqlCommand queryDeleteHorario = new SqlCommand(deleteHorario))
                    {
                        queryDeleteHorario.Connection = con;
                        con.Open();
                        queryDeleteHorario.ExecuteNonQuery();
                        con.Close();
                        return true;
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
