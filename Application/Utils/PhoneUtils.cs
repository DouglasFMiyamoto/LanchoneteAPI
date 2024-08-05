using System.Text.RegularExpressions;

namespace Application.Utils
{
    public static class PhoneUtils
    {
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            // Remove caracteres não numéricos
            phoneNumber = Regex.Replace(phoneNumber, @"[^\d]", "");

            // Verifica se o telefone tem 10 ou 11 dígitos
            if (phoneNumber.Length == 10 || phoneNumber.Length == 11)
            {
                // Expressão regular para validar números de telefone brasileiros
                // Telefones fixos: (XX) XXXX-XXXX
                // Telefones móveis: (XX) 9XXXX-XXXX
                var regex = new Regex(@"^(?:\(?[1-9]{2}\)?\s?)?(?:[2-8]|9[1-9])[0-9]{3}-?[0-9]{4}$");

                return regex.IsMatch(phoneNumber);
            }

            return false;
        }
    }
}
