using System.Text.RegularExpressions;

namespace MonitumAPI.Utils
{
    /// <summary>
    /// Classe que visa implementar todas as funções de validação de inputs (emails, passwords, etc.)
    /// </summary>
    public class InputValidator
    {
        public static Boolean emailChecker(string email)
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            return regex.IsMatch(email);
        }
    }
}
