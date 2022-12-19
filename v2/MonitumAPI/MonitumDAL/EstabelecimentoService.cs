using MonitumBOL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumDAL
{
    public class EstabelecimentoService
    {
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server e obter todos os registos de esabelecimentos lá criados (tabela Estabelecimento)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista de gestores</returns>
        public static async Task<List<Estabelecimento>> GetAllEstabelecimentos(string conString)
        {
            var estabelecimentoList = new List<Estabelecimento>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Estabelecimento", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Estabelecimento estabelecimento = new Estabelecimento();

                    estabelecimento.IdEstabelecimento = Convert.ToInt32(rdr["id_establecimento"]);
                    estabelecimento.Nome = rdr["nome"].ToString();
                    estabelecimento.Morada = rdr["morada"].ToString();

                    estabelecimentoList.Add(estabelecimento);


                }
                rdr.Close();
                con.Close();
            }

            return estabelecimentoList;
        }
    }
}
