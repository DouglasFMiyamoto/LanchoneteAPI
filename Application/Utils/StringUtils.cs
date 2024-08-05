using System.Text.RegularExpressions;

namespace Application.Utils
{
    public class StringUtils
    {
        public static String RemoverCaracteresEspeciais(String str) 
        {            
            return Regex.Replace(str, "[^0-9]", string.Empty); ;
        }
    }
}
