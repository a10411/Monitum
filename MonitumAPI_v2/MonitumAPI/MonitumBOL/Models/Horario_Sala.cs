using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a um Horario_Sala (Horário de uma Sala)
    /// Implementa a class (model) Gestor e os seus construtores
    /// </summary>
    public class Horario_Sala
    {
        public int IdHorario { get; set; }
        public int IdSala { get; set; }
        public string? DiaSemana { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSaida { get; set; }

        public Horario_Sala() { }

        /// <summary>
        /// Construtor que visa criar um Horario_Sala convertendo dados obtidos a partir de um SqlDataReader
        /// Este construtor é bastante útil no DAL, onde recebemos dados da base de dados e pretendemos converte-los num objeto
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Horario_Sala(SqlDataReader rdr)
        {
            this.IdHorario = Convert.ToInt32(rdr["id_horario"]);
            this.IdSala = Convert.ToInt32(rdr["id_sala"]);
            this.DiaSemana = rdr["dia_semana"].ToString() ?? String.Empty;
            this.HoraEntrada = Convert.ToDateTime(rdr["hora_entrada"].ToString()); // testar
            this.HoraSaida = Convert.ToDateTime(rdr["hora_saida"].ToString()); // testar
        }

    }
}
