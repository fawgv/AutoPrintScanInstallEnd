using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSALibrary.Printers
{
    public class PrinterFarm
    {
        public static void InstallPrinter(string ip, string printerDriverName, string pathToDriver, string fileNameDriver)
        {
            Console.WriteLine(PrinterMethods.ClearSpooler());
            Console.WriteLine(PrinterMethods.RemoveAllPrinters());
            Console.WriteLine(PrinterMethods.CreatePrinterPort(ip));
            Console.WriteLine(PrinterMethods.AddPrinterDriver(printerDriverName, CommonMethods.GetVersionOS(), pathToDriver, fileNameDriver));
            Console.WriteLine(PrinterMethods.ConnectPrinter(ip, printerDriverName));
            Console.WriteLine(PrinterMethods.SetDefaultPrinter(printerDriverName));

        }

        public static void InstallPrinter(string ip)
        {
            var curPSVC = PrinterScannerVariablesClass.CreatePrinterScannerVariablesClass(ip);
            InstallPrinter(ip, curPSVC.PrinterDriverName, curPSVC.PathToDrivers, curPSVC.PrinterDriverFileName);
        }
    }
}
