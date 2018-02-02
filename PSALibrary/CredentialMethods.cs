using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PSALibrary
{
    public class CredentialMethods
    {
        /// <summary>
        /// Метод безопасного чтения пароля, применяется для передачи пароля приложению
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static SecureString ReadPassword(string password)
        {
            SecureString str = new SecureString();
            for (int i = 0; i < password.Length; i++)
            {
                str.AppendChar(password[i]);
            }
            return str;
        }

    }
}
