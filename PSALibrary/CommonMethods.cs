using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSALibrary
{
    public class CommonMethods
    {

        #region Методы



        /// <summary>
        /// Метод запуска программы
        /// </summary>
        /// <param name="fileName">Имя программы, которая будет запускаться</param>
        /// <param name="arguments">Аргументы программы</param>
        /// <param name="wait">Параметр ожидания завершения программы</param>
        /// <param name="visible">Параметр видимости программы</param>
        /// <param name="directory">Путь к программе</param>
        /// <param name="admin">Параметр запуска программы под правами администратора</param>
        /// <param name="username">Пользователь, под которым будет запускаться программа</param>
        /// <param name="password">Пароль пользователя, при запуске с правами админа</param>
        /// <returns>Код завершения программы, при корректном завершении программы = 0</returns>
        public static int ExecuteProgram(string fileName, string arguments = "", bool wait = false, bool visible = true, string directory = "", bool admin = false, string username = "", string password = "")
        {
            Process process = null;
            int exitCode = 0;
            try
            {
                process = new Process
                {
                    StartInfo = { FileName = fileName }
                };
                if (directory != string.Empty)
                {
                    process.StartInfo.WorkingDirectory = directory;
                }
                if (!string.IsNullOrWhiteSpace(arguments))
                {
                    process.StartInfo.Arguments = arguments;
                }
                if (!visible)
                {
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                }
                if (admin)
                {
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.Verb = "runas";
                }
                if ((!admin && !string.IsNullOrWhiteSpace(username)) && !string.IsNullOrWhiteSpace(password))
                {
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.UserName = username;
                    process.StartInfo.Password = CredentialMethods.ReadPassword(password);
                    process.StartInfo.LoadUserProfile = true;
                }
                process.Start();
                if (wait)
                {
                    process.WaitForExit();
                    exitCode = process.ExitCode;
                }
            }
            catch (Exception)
            {
                exitCode = 1;
            }
            finally
            {
                if (process != null)
                {
                    process.Close();
                }
            }
            return exitCode;
        }

        public static string GetVersionOS()
        {
            if (Environment.Is64BitOperatingSystem)
                return "Windows x64";
            else return "Windows NT x86";
        }

        #endregion

    }
}
