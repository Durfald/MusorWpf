using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFService2;

namespace Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("LogPassUsers.txt"))
            {
                File.Create("LogPassUsers.txt").Close();
                File.WriteAllText("LogPassUsers.txt", "[]");
            }
            using (var host = new ServiceHost(typeof(ServiceData)))
            {
                
                host.Open();
                Console.WriteLine("Host started");
                Console.ReadLine();
            }
        }
    }
}
