using System;
using IWshRuntimeLibrary;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Management;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PSALibrary
{
    public class NetworkMethods
    {
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen);

        public static string GetHostNameFromIP(string ip)
        {
            try
            {
                IPHostEntry host1 = Dns.GetHostEntry(ip);
                return host1.HostName;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }



        //public static string GetMacHostFromIP(string ip)
        //{
        //    IPAddress dst = IPAddress.Parse(ip); // Требуемый IP адрес

        //    byte[] macAddr = new byte[6];
        //    uint macAddrLen = (uint)macAddr.Length;

        //    if (SendARP(BitConverter.ToInt32(dst.GetAddressBytes(), 0), 0, macAddr, ref macAddrLen) != 0)
        //        throw new InvalidOperationException("SendARP failed.");

        //    string[] str = new string[(int)macAddrLen];
        //    for (int i = 0; i < macAddrLen; i++)
        //        str[i] = macAddr[i].ToString("x2");

        //    return (string.Join(":", str));
        //}

        public static string GetSimpleMacHostFromIP(string ip)
        {
            try
            {
                IPAddress dst = IPAddress.Parse(ip); // Требуемый IP адрес

                byte[] macAddr = new byte[6];
                uint macAddrLen = (uint)macAddr.Length;

                if (SendARP(BitConverter.ToInt32(dst.GetAddressBytes(), 0), 0, macAddr, ref macAddrLen) != 0)
                    throw new InvalidOperationException("SendARP failed.");

                string[] str = new string[(int)macAddrLen];
                for (int i = 0; i < macAddrLen; i++)
                    str[i] = macAddr[i].ToString("x2");

                return (string.Join("", str));
            }
            catch (Exception)
            {
                return "Ошибка получения mac адреса";
            }
            
        }

        /// <summary>
        /// Метод проверки корректности введенного IP адреса
        /// </summary>
        /// <param name="ipAddress">строковый вариант IP адреса</param>
        /// <returns>возвращает булевое значение корректности IP адреса</returns>
        public static bool VerifyCorrectIpAddressInString(string ipAddress)
        {
            //Инициализируем новый экземпляр класса System.Text.RegularExpressions.Regex
            //для указанного регулярного выражения.
            Regex IpMatch = new Regex(@"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)");
            //Regex IpMatch = new Regex("^([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])$");
            //Выполняем проверку обнаружено ли в указанной входной строке 
            //соответствие регулярному выражению, заданному в
            //конструкторе System.Text.RegularExpressions.Regex.
            //если да то возвращаем true, если нет то false
            return IpMatch.IsMatch(ipAddress);
        }
    }
}
