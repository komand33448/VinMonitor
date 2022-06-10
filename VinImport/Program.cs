using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace VinImport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DVIService.monitorSoapClient ds = new DVIService.monitorSoapClient();
            Console.WriteLine(ds.StockTemp().ToString("N2") + "C");
            Console.ReadLine();
        }
    }
}

//http://dvimonitor.pilotdrift.dk/monitor.asmx