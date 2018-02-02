using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSALibrary.Scanners
{
    public class ScannerMethods
    {
        /// <summary>
        /// Добавляем устройство по ID указав драйвер в диспетчер устройств
        /// </summary>
        /// <param name="pathToDriver">Полный путь к драйверу принтера</param>
        /// <param name="fileNameDriver">имя файла драйвера</param>
        /// <param name="vendorID">VID формата VID_XXXX&PID_XXXX&IP_SCAN</param>
        /// <returns></returns>
        public static string AddScannerByIDFromDriver(string pathToDriver, string fileNameDriver, string vendorID)
        {
            string pathToDevCon = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"devcon.exe");
            string combinePath = Path.Combine(pathToDriver, fileNameDriver);
            //string argument = $"/r install {combinePath} \"{vendorID}\"";
            string argument = $"/r install {combinePath} \"{vendorID}\"";
            int result = CommonMethods.ExecuteProgram(pathToDevCon, argument, true);
            return $"Добавление сканера в диспетчер устройств {vendorID} завершено с кодом {result}";
        }

        /// <summary>
        /// Метод тихой установки SCAN_APP
        /// </summary>
        /// <param name="pathToAPP">путь до пакета</param>
        /// <param name="fileAPP">имя файла пакета Scan_APP</param>
        /// <returns></returns>
        public static string InstallScanApp(string pathToAPP, string fileAPP, Enums.MFUTypesEnum mFUTypesEnum = Enums.MFUTypesEnum.HP)
        {
            string combinePath = Path.Combine(pathToAPP, fileAPP);
            string argument = string.Empty;
            if (mFUTypesEnum == Enums.MFUTypesEnum.HP)
            {
                argument = $"/qn /norestart";
            }
            else if (mFUTypesEnum == Enums.MFUTypesEnum.Kyocera)
            {
                argument = $"/L1049 /S /v /qn";
            }
            
            int result = CommonMethods.ExecuteProgram(combinePath, argument, true);
            return $"Добавление сканера в диспетчер устройств {combinePath} завершено с кодом {result}";
        }

        /// <summary>
        /// Удаляет все устройства из диспетчера устройств с указанным VID
        /// </summary>
        /// <param name="vid">VID устройства вида VID_03F0&PID_2d2a&IP_SCAN</param>
        /// <returns></returns>
        public static string DevConRemoveScaners(string vid)
        {
            string pathToDevCon = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"devcon.exe");
            string argument = $"remove \"{vid}\"";
            int result = CommonMethods.ExecuteProgram(pathToDevCon, argument, true);
            return $"Удаление всех устройств {vid} из диспетчера устройств завершено с кодом {result}";
        }


       


    }
}
