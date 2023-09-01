using System.Text.RegularExpressions;

namespace Nexus.Auth.Api.Helpers
{
    public static class EmailChecker
    {
        public static bool IsValidEmail(string email)
        {
            // Expressão regular para validar o formato do e-mail
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Utilizando a classe Regex para verificar o padrão do e-mail
            Match match = Regex.Match(email, pattern);

            // Se o e-mail corresponder ao padrão, é considerado válido
            return match.Success;
        }
    }
}
