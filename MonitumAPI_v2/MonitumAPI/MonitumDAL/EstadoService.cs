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
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes ao Estado
    /// Isto é, todos os acessos à base de dados relativos ao estado estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class EstadoService
    {
       
        public static async Task<List<Estado>> GetAllEstados(string conString)
        {
            var estadoList = new List<Estado>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Estado", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Estado estado = new Estado(rdr);
                    estadoList.Add(estado);
                }
                rdr.Close();
                con.Close();
            }

            return estadoList;
        }





    }
}
