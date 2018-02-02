using PSALibrary.CreateFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSALibrary.Scanners
{
    public class KMTwainFarm
    {
        private static KMTwainFarm instance;

        private string ip;
        private string printerDriverName;
        private KMTwainFileClass kMTwainFileClass;

        private KMTwainFarm()
        {

        }

        public static KMTwainFarm CreateKMTwainFarm(string curIp, string curPrinterDriverName)
        {
            if (instance == null)
            {
                instance = new KMTwainFarm();
                instance.ip = curIp;
                instance.printerDriverName = curPrinterDriverName;
            }

            if (curPrinterDriverName.Contains("FS-1030MFP"))
            {
                instance.CreateKMTwainFileClass(0, 4, 1, 1, "FS-1030MFP/FS-1130MFP", 0);
                instance.CreateKM_TWAIN1();
                instance.CreateKM_RegList();
            }
            
            return instance;

        }

        private void CreateKMTwainFileClass(int unit, int type, int defaultUse, int regNum, string model, int pos)
        {
            instance.kMTwainFileClass = new KMTwainFileClass()
            {
                Unit = unit,
                ScannerAddress = ip,
                Type = type,
                DefaultUse = defaultUse,
                RegNum = regNum,
                Name = printerDriverName,
                Model = model,
                Pos = pos
            };
        }

        #region Формирование файлов настроек для KM_Twain

        private string CreateKM_TWAIN1()
        {
            var stringsArray = new string[]
            {
                "[Contents]",
                $"Unit={kMTwainFileClass.Unit}",
                $"ScannerAddress={kMTwainFileClass.ScannerAddress}",
                "SSL=0",
                "[Authentication]",
                "Auth=0",
                "UserName=",
                "Account=0",
                "ID=",
                "Password=43srWkUjR/8=",
            };

            var fileName = "KM_TWAIN1.ini";
            var path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), @"Kyocera\KM_TWAIN");
            return FileMethods.CreateFileSettings(fileName, path, stringsArray);

        }

        public string CreateKM_RegList()
        {
            var stringsArray = new string[]
            {
                "[Setting]",
                $"Type={kMTwainFileClass.Type}",
                $"DefaultUse={kMTwainFileClass.DefaultUse}",
                $"RegNum={kMTwainFileClass.RegNum}",
                "[Scanner1]",
                $"Name={kMTwainFileClass.Name}",
                $"Model={kMTwainFileClass.Model}",
                "DefFile=KM_TWAIN1.ini",
                "LastScan=N_LSTSCN1.xml",
                "ScanList=N_SCNLST1.xml",
                $"Pos={kMTwainFileClass.Pos}"
            };

            var fileName = "RegList.ini";
            var path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), @"Kyocera\KM_TWAIN");
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return FileMethods.CreateFileSettings(fileName, path, stringsArray);
        }

        #endregion
        //        [Contents]
        //Unit=0
        //ScannerAddress=172.16.9.145
        //SSL=0
        //[Authentication]
        //Auth=0
        //UserName=
        //Account=0
        //ID=
        //Password=43srWkUjR/8=
    }
}
