using PSALibrary;
using PSALibrary.Printers;
using PSALibrary.Scanners;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PrinterScannerAutoInstall
{
    class Program
    {
        static void Main(string[] args)
        {
            bool start = true;

            //string ipAddr = "172.16.9.145";
            //string ipAddr = "172.16.10.132";
            //var printerDriverName = "HP LaserJet Pro MFP M225-M226 PCL 6";
            //var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\HP\HP_LJ_Pro_MFP_M225-M226");
            //var printerDriverFileName = "hpcm225u.inf";

            //var scannerDriverName = "HP LJ M225M226 Scan Drv";
            //var scannerDriverFileName = "hppasc_lj225226.inf";
            //var scannerVID = "VID_03F0&PID_2d2a&IP_SCAN";

            //var pathScanApp = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\HP\HP_LJ_Pro_MFP_M225-M226\Scan_App");
            //var scanAppFileName = "HPScanLJ225226.msi";

            //Console.WriteLine($"Printer name: {NetworkMethods.GetHostNameFromIP(ipAddr)}");
            //Console.WriteLine($"Mac address: {NetworkMethods.GetSimpleMacHostFromIP(ipAddr)}");
            //Console.WriteLine(SNMPMethods.GetPrinterModel("172.16.10.88"));
            //Console.WriteLine(SNMPMethods.GetPrinterModel(ipAddr));
            //Console.ReadKey();
            //Console.WriteLine(SNMPMethods.GetPrinterModel(ipAddr));

            if (start)
            {
                VerifyArgsClass verifyArgsClass = new VerifyArgsClass();
                verifyArgsClass.VerifyArgs(args);

                

                //string mac = NetworkMethods.GetSimpleMacHostFromIP(ipAddr);
                //string printerName = NetworkMethods.GetHostNameFromIP(ipAddr);
                
            }


            Console.ReadKey();
            
        }


    }
}
