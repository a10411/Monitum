using System.Text.RegularExpressions;

namespace MonitumAPI.Utils
{
    /// <summary>
    /// Classe que visa implementar todas as funções de validação de inputs (emails, passwords, etc.)
    /// </summary>
    public class InputValidator
    {
        /// <summary>
        /// Função que visa verificar se um email é válido
        /// </summary>
        /// <param name="email">Email a verificar</param>
        /// <returns>True se email for válido, False se email não for válido</returns>
        public static Boolean emailChecker(string email)
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            return regex.IsMatch(email);
        }

        /// <summary>
        /// Função que visa verificar se um weekday introduzido pelo utilizador é válido (para inserção de horários)
        /// </summary>
        /// <param name="weekday">Weekday a introduzir</param>
        /// <returns>True se válido, false se inválido</returns>
        public static Boolean weekdayPTChecker(string weekday)
        {
            switch (weekday)
            {
                case "seg":
                    return true;
                case "ter":
                    return true;
                case "qua":
                    return true;
                case "qui":
                    return true;
                case "sex":
                    return true;
                case "sab":
                    return true;
                case "dom":
                    return true;
                default:
                    return false;
            }
        }
    }
}
