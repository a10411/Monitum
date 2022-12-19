﻿using MonitumBOL.Models;
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
    }
}
