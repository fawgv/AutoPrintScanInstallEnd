using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSALibrary.CreateFiles
{
    public class KMTwainFileClass
    {
        public int Unit { get; set; }
        public string ScannerAddress { get; set; }
        public int Type { get; set; }
        public int DefaultUse { get; set; }
        public int RegNum { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int Pos { get; set; }
    }
}
