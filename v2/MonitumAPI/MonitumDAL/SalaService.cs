using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitumBOL.Models;

namespace MonitumDAL
{
    public class SalaService
    {
        public static async Task<Boolean> AddSala(string conString, Sala salaToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addSala = "INSERT INTO Sala (id_estabelecimento, id_estado) VALUES (@idEstabelecimento, @idEstado)";
                    using (SqlCommand queryAddSala = new SqlCommand(addSala))
                    {
                        queryAddSala.Connection = con;
                        queryAddSala.Parameters.Add("@idEstabelecimento", SqlDbType.Int).Value = salaToAdd.IdEstabelecimento;
                        queryAddSala.Parameters.Add("@idEstado", SqlDbType.Int).Value = salaToAdd.IdEstado;
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
    }
}
