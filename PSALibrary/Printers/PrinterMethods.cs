using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Management;
using System.Diagnostics;
using System.Net;

namespace PSALibrary.Printers
{
    public class PrinterMethods
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetDefaultPrint(string Name);

        /// name of the printer we want to be default/// Returns true if successfull
        /// Throws exception if printer not installed
        private static bool SetPrinterToDefault(string printer)
        {
            //path we need for WMI
            string queryPath = "win32_printer.DeviceId='" + printer + "'";

            try
            {
                //ManagementObject for doing the retrieval
                ManagementObject managementObj = new ManagementObject(queryPath);

                //ManagementBaseObject which will hold the results of InvokeMethod
                ManagementBaseObject obj = managementObj.InvokeMethod("SetDefaultPrinter", null, null);

                //if we're null the something went wrong
                if (obj == null)
                    throw new Exception("Unable to set default printer.");

                //now get the return value and make our decision based on that
                int result = (int)obj.Properties["ReturnValue"].Value;

                if (result == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string SetDefaultPrinter(string name)
        {
            if (SetPrinterToDefault(name))
                return $"Принтер {name} установлен принтером по умолчанию";
            else
                return "Во время установки принтера по умолчанию произошла ошибка.";
        }

        /// <summary>
        /// Метод создания порта принтера
        /// </summary>
        /// <param name="ip">IP адрес принтера</param>
        public static string CreatePrinterPort(string ip)
        {
            string argument = $"C:\\Windows\\System32\\Printing_Admin_Scripts\\ru-RU\\prnport.vbs -a -r \"{ip}\" -h \"{ip}\" -o RAW -n 9100";
            int result = CommonMethods.ExecuteProgram("cscript.exe", argument,true);
            return $"Создание порта {ip} завершено с кодом {result}";

        }

        /// <summary>
        /// Метод добавления драйвера принтера в список драйверов
        /// </summary>
        /// <param name="printerDriverName">имя принтера должно указываться точно так же как оно прописано в inf файле драйвера</param>
        /// <param name="typeOS">разрядность ОС</param>
        /// <param name="pathToDriver">Полный путь к драйверу принтера</param>
        /// <param name="fileNameDriver">имя файла драйвера</param>
        public static string AddPrinterDriver(string printerDriverName, string typeOS, string pathToDriver, string fileNameDriver)
        {
            string combinePath = Path.Combine(pathToDriver, fileNameDriver);
            string argument = $"C:\\Windows\\System32\\Printing_Admin_Scripts\\ru-RU\\prndrvr.vbs -a -m \"{printerDriverName}\" -e \"{typeOS}\" -h \"{pathToDriver}\" -i \"{combinePath}\"";
            int result = CommonMethods.ExecuteProgram("cscript.exe", argument, true);
            return $"Добавление драйвера принтера {printerDriverName} завершено с кодом {result}";
        }

        /// <summary>
        /// Метод подключения принтера
        /// </summary>
        /// <param name="ip">IP адрес принтера</param>
        /// <param name="printerDriverName">имя принтера должно указываться точно так же как оно прописано в inf файле драйвера</param>
        public static string ConnectPrinter(string ip, string printerDriverName)
        {
            string argument = $"printui.dll,PrintUIEntry /if /b \"{printerDriverName}\" /r \"{ip}\" /m \"{printerDriverName}\" /u /K /q /Gw";
            int result = CommonMethods.ExecuteProgram("rundll32.exe", argument, true);
            return $"Подключение принтера {printerDriverName} завершено с кодом {result}";
        }

        public static string ClearSpooler()
        {
            string argument = " /c net stop spooler & del %systemroot%\\system32\\spool\\printers\\* /Q & net start spooler";
            int result = CommonMethods.ExecuteProgram("cmd.exe", argument, true);
            return $"Выполнена очистка очереди печати Spooler, завершено с кодом {result}";
        }

        public static string RemoveAllPrinters()
        {
            try
            {
                ManagementClass win32Printer = new ManagementClass("Win32_Printer");
                ManagementObjectCollection printers = win32Printer.GetInstances();
                foreach (ManagementObject printer in printers)
                {
                    printer.Delete();
                }
            }
            catch (Exception)
            {
                return "Ошибка удаления всех принтеров";
            }
            return "Все принтеры удалены из \"Устройства и принтеры\"";
            
        }

        public static void RemoveNetworkPrinter(string localName)
        {

            WshNetwork network = new WshNetwork();

            object force = true;
            object updateProfile = true;

            network.RemovePrinterConnection(localName, ref force, ref updateProfile);

            Marshal.ReleaseComObject(network);
        }

        public static void PrinterList()
        {
            // USING WMI. (WINDOWS MANAGEMENT INSTRUMENTATION)

            System.Management.ManagementScope objMS =
                new System.Management.ManagementScope(ManagementPath.DefaultPath);
            objMS.Connect(); 

            SelectQuery objQuery = new SelectQuery("SELECT * FROM Win32_Printer");
            ManagementObjectSearcher objMOS = new ManagementObjectSearcher(objMS, objQuery);
            System.Management.ManagementObjectCollection objMOC = objMOS.Get();

            foreach (ManagementObject Printers in objMOC)
            {
                //if (Convert.ToBoolean(Printers["Local"]))       // LOCAL PRINTERS.
                //{
                //    cmbLocalPrinters.Items.Add(Printers["Name"]);
                //}
                //if (Convert.ToBoolean(Printers["Network"]))     // ALL NETWORK PRINTERS.
                //{
                //    cmbNetworkPrinters.Items.Add(Printers["Name"]);
                //}
                Debug.WriteLine(Printers);
            }
        }



        



    }
}
