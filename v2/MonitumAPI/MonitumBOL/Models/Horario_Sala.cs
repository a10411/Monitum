using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBOL.Models
{
    public class Horario_Sala
    {
        public int IdHorario { get; set; }
        public int IdSala { get; set; }
        public string DiaSemana { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSaida { get; set; }

        public Horario_Sala() { }

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
