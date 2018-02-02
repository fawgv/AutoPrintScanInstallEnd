using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PSALibrary
{
    public class SNMPMethods
    {





        //Dictionary<string, ExecuteProcess> dictionaryPrinters = new Dictionary<string, ExecuteProcess>();

        //void FillDictionaryPrinters()
        //{

        //    //HP
        //    dictionaryPrinters.Add("m225", new ExecuteProcess(TypeExecuteProcess.Process, @"c:\_Drivers\HP 225\HP_LJ_Pro_MFP_M225-M226_Full_Solution_16078.exe"));
        //    dictionaryPrinters.Add("m121", new ExecuteProcess(TypeExecuteProcess.Process, @"c:\_Drivers\HP 1212-1217\LJM1130_M1210_MFP_Full_Solution.exe"));
        //    dictionaryPrinters.Add("m426", new ExecuteProcess(TypeExecuteProcess.Process, @"c:\_Drivers\HP 426-427\HP_LJ_Pro_MFP_M426f-M427f-PCL_6_v3_Full_Solution_17171.exe"));
        //    dictionaryPrinters.Add("m1536", new ExecuteProcess(TypeExecuteProcess.Link, @"https://support.hp.com/ru-ru/drivers/selfservice/hp-laserjet-pro-m1536-multifunction-printer-series/3974271/model/3974278"));
        //    dictionaryPrinters.Add("m127", new ExecuteProcess(TypeExecuteProcess.Process, @"C:\_Drivers\HP 126-127\LJPro_MFP_M127-M128_full_solution_15309.exe"));
        //    dictionaryPrinters.Add("m2727", new ExecuteProcess(TypeExecuteProcess.Link, @"https://support.hp.com/ru-ru/drivers/selfservice/hp-laserjet-m2727-multifunction-printer-series/3377075/model/3377076"));
        //    dictionaryPrinters.Add("3055", new ExecuteProcess(TypeExecuteProcess.Link, @"https://support.hp.com/ru-ru/drivers/selfservice/hp-laserjet-3055-all-in-one-printer/1161389"));

        //    //Brother
        //    dictionaryPrinters.Add("mfc-7860", new ExecuteProcess(TypeExecuteProcess.Process, @"c:\_Drivers\Brother 7860\MFC-7860DW-inst-C1-EEU.EXE"));
        //    dictionaryPrinters.Add("mfc-7360", new ExecuteProcess(TypeExecuteProcess.Link, @"http://support.brother.com/g/b/downloadlist.aspx?c=ru&lang=ru&prod=mfc7360nr_eu&os=7"));
        //    dictionaryPrinters.Add("dcp-7065", new ExecuteProcess(TypeExecuteProcess.Process, @"c:\_Drivers\Brother 7065\DCP-7065DN-inst-C1-EEU.EXE"));

        //    //Kyocera
        //    dictionaryPrinters.Add("m2030", new ExecuteProcess(TypeExecuteProcess.Folder, @"c:\_Drivers\Kyocera M2035dn"));
        //    dictionaryPrinters.Add("m2035", new ExecuteProcess(TypeExecuteProcess.Folder, @"c:\_Drivers\Kyocera M2035dn"));
        //    dictionaryPrinters.Add("m2530", new ExecuteProcess(TypeExecuteProcess.Folder, @"c:\_Drivers\Kyocera M2035dn"));
        //    dictionaryPrinters.Add("m2535", new ExecuteProcess(TypeExecuteProcess.Folder, @"c:\_Drivers\Kyocera M2035dn"));
        //    dictionaryPrinters.Add("m2040", new ExecuteProcess(TypeExecuteProcess.Folder, @"c:\_Drivers\Kyocera 2040 Twain"));

        //    //Samsung
        //    dictionaryPrinters.Add("scx-4x24", new ExecuteProcess(TypeExecuteProcess.Link, @"http://www.samsung.com/ru/support/model/SCX-4824FN/XEV"));
        //    dictionaryPrinters.Add("scx-483", new ExecuteProcess(TypeExecuteProcess.Link, @"http://www.samsung.com/ru/support/model/SCX-4833FD/XEV"));

        //}

        public static string GetPrinterModel(string IP)
        {
            string oid = "1.3.6.1.2.1.25.3.2.1.3.1";
            string printerModel = string.Empty;
            try
            {
                var result = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(IP), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier(oid)) },
                           2000);
                string oidresult = string.Empty;
                foreach (var item in result)
                {
                    oidresult += item.ToString();
                }
                printerModel = oidresult;

                var ie = result[0].ToString();
                var iee = ie.Split(':');
                //Console.WriteLine(iee[3]);
                printerModel = iee[3].Trim();
            }
            catch (Exception)
            {

            }
            return printerModel;
        }

        
    }
}
