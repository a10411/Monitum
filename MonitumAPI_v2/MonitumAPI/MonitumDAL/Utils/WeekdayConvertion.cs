using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumDAL.Utils
{
    /// <summary>
    /// Classe que visa implementar as funções de conversão do dia da semana
    /// </summary>
    public class WeekdayConvertion
    {
        /// <summary>
        /// Função que visa converter o dia da semana em inglês para português, no formato que temos na BD (seg, ter, qua, etc.)
        /// </summary>
        /// <param name="weekdayEng">Dia da semana em inglês</param>
        /// <returns>Dia da semana "traduzido", no formato da BD</returns>
        public static string WeekdayConverterEngToPT(string weekdayEng)
        {
            switch (weekdayEng)
            {
                case "Monday":
                    return "seg";
                case "Tuesday":
                    return "ter";
                case "Wednesday":
                    return "qua";
                case "Thursday":
                    return "qui";
                case "Friday":
                    return "sex";
                case "Saturday":
                    return "sab";
                case "Sunday":
                    return "dom";
                default:
                    return "NA";
            }
        }
    }
}
