using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSALibrary.Scanners
{
    public class ScannerFarm
    {
        public static void InstallScanner(
            string ip, 
            string scannerDriverName, 
            string pathToDriver, 
            string fileNameDriver, 
            string vid, 
            string namePrinter, 
            string mac, 
            string pathToAPP, 
            string fileAPP,
            Enums.MFUTypesEnum mFUTypesEnum = Enums.MFUTypesEnum.HP
            
            )
        {
            Console.WriteLine(ScannerMethods.DevConRemoveScaners(vid));

            Console.WriteLine(ScannerMethods.AddScannerByIDFromDriver(
                pathToDriver,
                fileNameDriver,
                vid));

            RegistryMethods.SetRegistryParameters(scannerDriverName, namePrinter, ip, mac);

            Console.WriteLine(ScannerMethods.InstallScanApp(pathToAPP, fileAPP, mFUTypesEnum));

            Console.WriteLine(KMTwainFarm.CreateKMTwainFarm(ip, namePrinter));
        }

        public static void InstallScanner(string ip)
        {
            var curPSVC = PrinterScannerVariablesClass.CreatePrinterScannerVariablesClass(ip);
            InstallScanner(ip,
                curPSVC.ScannerDriverName,
                curPSVC.PathToDrivers,
                curPSVC.ScannerDriverFileName,
                curPSVC.ScannerVID,
                curPSVC.PrinterDriverName,
                curPSVC.Mac,
                curPSVC.PathScanApp,
                curPSVC.ScanAppFileName,
                curPSVC.MFUTypesEnumProp
                );
        }
    }
}
