using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSALibrary
{
    public class RegistryMethods
    {
        public static void SetRegistryParameters(string driverScanner, string namePrinter, string ip, string mac)
        {
            try
            {
                #region Внесение изменений в блок [HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{6BDD1FC6-810F-11D0-BEC7-08002BE2092F}

                var image = Registry.LocalMachine.OpenSubKey("System").OpenSubKey("CurrentControlSet").OpenSubKey("Control").OpenSubKey("Class").OpenSubKey("{6BDD1FC6-810F-11D0-BEC7-08002BE2092F}", true);
                RegistryKey workKey = FindWorkKey(driverScanner, image);
                if (workKey != null)
                {
                    ConfiguratingRegistry(namePrinter, ip, mac, workKey, "DeviceData");
                }

                #endregion

                #region Внесение изменений в блок [HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\Root\IMAGE\

                var image2 = Registry.LocalMachine.OpenSubKey("SYSTEM").OpenSubKey("CurrentControlSet").OpenSubKey("Enum").OpenSubKey("Root").OpenSubKey("Image", true);
                RegistryKey workKey2 = FindWorkKey(driverScanner, image2);
                if (workKey != null)
                {
                    ConfiguratingRegistry(namePrinter, ip, mac, workKey2, "Device Parameters");
                }

                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удается открыть реестр, не достаточно прав. {ex.Message}");
            }
        }

        private static RegistryKey FindWorkKey(string driverScanner, RegistryKey inRegistryKey)
        {
            
            RegistryKey workKey = null;
            foreach (var item in inRegistryKey.GetSubKeyNames())
            {
                try
                {
                    var temp = inRegistryKey.OpenSubKey(item);
                    foreach (var key in temp.GetValueNames())
                    {
                        if (key == "DriverDesc" || key == "DeviceDesc")
                        {
                            if (temp.GetValue(key).ToString() == driverScanner)
                            {
                                workKey = temp;
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }
            }

            return workKey;
        }

        private static void ConfiguratingRegistry(string namePrinter, string ip, string mac, RegistryKey workKey, string dataParametersIn)
        {
            #region Добавление полей в реестр

            try
            {
                var dataParameters = workKey.OpenSubKey(dataParametersIn, true);
                dataParameters.SetValue("NetworkDeviceID", $"\\\\hostname:{namePrinter}\\\\ipaddr:{ip}\\\\guid:\\\\macaddr:{mac}\\port:1");
                dataParameters.SetValue("PortID", ip);
                dataParameters.SetValue("NetworkHostName", namePrinter);
                dataParameters.SetValue("TulipIOType", 00000005, RegistryValueKind.DWord);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Не удалось внести изменения в реестр {ex.Message} {ex.StackTrace}");
            }

            #endregion
        }

       
    }
}
