using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSALibrary
{
    public class PrinterScannerVariablesFarm
    {
        public static PrinterScannerVariablesClass CreatePrinterScannerClass(string ip, PrinterScannerVariablesClass psvClass)
        {
            var curFarm = new PrinterScannerVariablesFarm();
            psvClass.IpAddr = ip;
            psvClass.HostName = NetworkMethods.GetHostNameFromIP(ip);
            psvClass.Mac = NetworkMethods.GetSimpleMacHostFromIP(ip);

            curFarm.FillPrinterScannerFields(SNMPMethods.GetPrinterModel(ip), psvClass);
            return psvClass;

        }

        private void Fill(PrinterScannerVariablesClass psvClass, string pathToDrivers,
            string printerDriverName, string printerDriverFileName,
            string scannerDriverName, string scannerDriverFileName,
            string scannerVID, string pathScanApp, string scanAppFileName,
            Enums.MFUTypesEnum mFUTypesEnum = Enums.MFUTypesEnum.HP)
        {
            #region Заполнение общих свойств
            psvClass.PathToDrivers = pathToDrivers;
            psvClass.MFUTypesEnumProp = mFUTypesEnum;
            #endregion

            #region Заполнение свойств принтера
            psvClass.PrinterDriverName = printerDriverName;
            psvClass.PrinterDriverFileName = printerDriverFileName;
            #endregion

            #region Заполнение свойств сканера
            psvClass.ScannerDriverName = scannerDriverName;
            psvClass.ScannerDriverFileName = scannerDriverFileName;
            psvClass.ScannerVID = scannerVID;
            psvClass.PathScanApp = pathScanApp;
            psvClass.ScanAppFileName = scanAppFileName;
            #endregion
        }

        private void FillPrinterScannerFields(string printerModel, PrinterScannerVariablesClass psvClass)
        {
            var hp225 = new string[] { "HP LaserJet Pro MFP M225dw", "HP LaserJet Pro MFP M225rdn", "HP LaserJet Pro MFP M225dn" };
            string[] hp426f427f = new string[] { "HP LaserJet MFP M426fdn", "HP LaserJet MFP M426frdn", "HP LaserJet MFP M426fdw" };
            var hp127128 = new string[] { "HP LaserJet Pro MFP M127fn", "HP LaserJet Pro MFP M127f", "HP LaserJet Pro MFP M127fw" };
            var hp1210Series = new string[] { "HP LaserJet Professional M1212nf MFP", "HP LaserJet Professional M1213nf MFP", "HP LaserJet Professional M1214nfh",
                "HP LaserJet Professional M1216nfh MFP", "HP LaserJet Professional M1217nfw MFP",
                //"HP LaserJet Professional M1219nf MFP", "HP LaserJet Professional M1218nfs MFP",
                //"HP LaserJet Professional M1218nfs MFP", "HP LaserJet Professional M1219nfs MFP"
            };
            var kyoceraKyoceraFS1030MFP = new string[] { "Kyocera FS-1030MFP KX", "FS-1030MFP" };

            if (hp225.Contains(printerModel))
            {
                HP225Fill(psvClass);
            }
            else if (hp426f427f.Contains(printerModel))
            {
                HP426f427fFill(psvClass);
            }
            else if (hp127128.Contains(printerModel))
            {
                HP127128Fill(psvClass);
            }
            else if (hp1210Series.Contains(printerModel))
            {
                HP1210SeriesFill(psvClass);
            }
            else if (kyoceraKyoceraFS1030MFP.Contains(printerModel))
            {
                KyoceraFS1030MFPFill(psvClass);
            }

        }

        private void KyoceraFS1030MFPFill(PrinterScannerVariablesClass psvClass)
        {
            Fill(psvClass, System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\Kyocera\Printer\64bit\Vista and newer"),
                "Kyocera FS-1030MFP KX", "OEMSETUP.INF",
                string.Empty, string.Empty,
                string.Empty,
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\Kyocera\Scanner\TWAIN"),
                "setup.exe", 
                Enums.MFUTypesEnum.Kyocera
                );
        }


        private void HP1210SeriesFill(PrinterScannerVariablesClass psvClass)
        {
            Fill(psvClass, System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\HP\HP_LJ_Pro_MFP_1212-1217"),
                "HP LaserJet Professional M1212nf MFP", "HPM1210.INF",
                "HP LaserJet Professional M1210 MFP Series", "HP1210NS.INF",
                "HPM1210nScanner",
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\HP\HP_LJ_Pro_MFP_1212-1217\ScanTo\x64"),
                "ScanTo.msi"
                );
        }

        private void HP225Fill(PrinterScannerVariablesClass psvClass)
        {
            Fill(psvClass, System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\HP\HP_LJ_Pro_MFP_M225-M226"),
                "HP LaserJet Pro MFP M225-M226 PCL 6", "hpcm225u.inf",
                "HP LJ M225M226 Scan Drv", "hppasc_lj225226.inf",
                "VID_03F0&PID_2d2a&IP_SCAN",
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\HP\HP_LJ_Pro_MFP_M225-M226\Scan_App"),
                "HPScanLJ225226.msi"
                );
        }



        private void HP426f427fFill(PrinterScannerVariablesClass psvClass)
        {
            Fill(psvClass, System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\HP\HP_LJ_Pro_MFP_M426f-M427f"),
                "HP LaserJet Pro MFP M426f-M427f PCL 6", "hpma5a2a_x64.inf",
                "HP LJ M426fM427f Scan Drv", "hppasc_lj426427f.inf",
                "VID_03F0&PID_5a2a&IP_SCAN",
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\HP\HP_LJ_Pro_MFP_M426f-M427f\Scan_App"),
                "HPScanLJ426427f.msi"
                );
        }

        private void HP127128Fill(PrinterScannerVariablesClass psvClass)
        {
            Fill(psvClass, System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\HP\HP_LJ_Pro_MFP_M127-M128"),
                "HP LaserJet Pro MFP M127-M128 PCLmS", "hpcm127128.inf",
                "HP LJ M127128 Scan Drv", "hppasc_lj127128.inf",
                "VID_03F0&PID_322a&IP_SCAN",
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Drivers\HP\HP_LJ_Pro_MFP_M127-M128\Scan_App"),
                "HPScanLJ127128.msi"
                );
        }
    }
}
