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
    public class Horario_SalaService
    {
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

        public static async Task<Horario_Sala> UpdateHorario(string conString, Horario_Sala horarioUpdated)
        {
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
        /// Método que visa adicionar um horário a uma sala de um estabelecimento
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="horarioToAdd">Horário a adicionar à sala</param>
        /// <returns>True se tudo tiver corrido bem (horário adicionado), algum erro caso o horário não tenha sido adicionado.</returns>
        public static async Task<Boolean> AddHorario(string conString, Horario_Sala horarioToAdd)
        {
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
            catch (Exception e)
            {
                throw;
            }


        }


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
