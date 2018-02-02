using PSALibrary;
using PSALibrary.Printers;
using PSALibrary.Scanners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterScannerAutoInstall
{
    public class VerifyArgsClass
    {
        public VerifyArgsClass()
        {

        }

        public void VerifyArgs(string[] args)
        {
            if (IsContainsIP(args))
            {
                #region Установка принтера
                PrinterFarm.InstallPrinter(GetIPFromArgs(args));
                #endregion

                #region Установка сканера
                ScannerFarm.InstallScanner(GetIPFromArgs(args));
                #endregion

                if (SNMPMethods.GetPrinterModel(GetIPFromArgs(args)) == "HP LaserJet Professional M1212nf MFP")
                {
                    string twainFileSource = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory, 
                        @"Drivers\HP\HP_LJ_Pro_MFP_1212-1217\Russian\ScanDriver\M1210.ds"
);
                    string twainFileDestination = @"C:\Windows\twain_32\hpm1210nf\m1210.ds";
                    if (!System.IO.Directory.Exists(@"C:\Windows\twain_32\hpm1210nf"))
                    {
                        System.IO.Directory.CreateDirectory(@"C:\Windows\twain_32\hpm1210nf");
                        var cred = new System.Security.AccessControl.DirectorySecurity(@"C:\Windows\twain_32\hpm1210nf", System.Security.AccessControl.AccessControlSections.All);

                        System.IO.Directory.CreateDirectory(@"C:\Windows\twain_32\hpm1210nf", cred);
                    }
                    System.IO.File.Copy(twainFileSource, twainFileDestination, true);
                }
            }
            else
            {
                PrintHelp();
            }
        }

        public string GetIPFromArgs(string[] args)
        {
            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "/i" && args[i + 1] != null && NetworkMethods.VerifyCorrectIpAddressInString(args[i + 1]))
                    {
                        return args[i + 1];
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return string.Empty;
        }

        public bool IsContainsIP(string[] args)
        {
            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "/i" && args[i + 1] != null && NetworkMethods.VerifyCorrectIpAddressInString(args[i + 1]))
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public void PrintHelp()
        {
            Console.WriteLine(@"//? Помощь");
            Console.WriteLine("[//i IP адрес] Выполняет установку драйвера принтера и сканера, если указанный IP адрес принадлежит МФУ");

        }
    }
}
