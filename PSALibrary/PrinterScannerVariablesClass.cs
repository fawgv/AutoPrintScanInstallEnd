using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSALibrary
{
    public class PrinterScannerVariablesClass
    {
        #region Поля

        #endregion

        #region Свойства

        public string IpAddr { get; set; }
        public string HostName { get; set; }
        public string Mac { get; set; }
        public string PrinterDriverName { get; set; }
        public string PathToDrivers { get; set; }
        public string PrinterDriverFileName { get; set; }
        public string ScannerDriverName { get; set; }
        public string ScannerDriverFileName { get; set; }
        public string ScannerVID { get; set; }
        public string PathScanApp { get; set; }
        public string ScanAppFileName { get; set; }
        public Enums.MFUTypesEnum MFUTypesEnumProp { get; set; }

        #endregion

        #region Конструкторы
        private PrinterScannerVariablesClass()
        {

        }
        #endregion

        #region Методы

        public static PrinterScannerVariablesClass CreatePrinterScannerVariablesClass(string ip)
        {
            return PrinterScannerVariablesFarm.CreatePrinterScannerClass(ip, new PrinterScannerVariablesClass());
        }

        #endregion



    }
}
