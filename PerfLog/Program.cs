using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace PerfLog
{
    public class ProcessMonitor
    {
        WindowsAPIs.Utils u = new WindowsAPIs.Utils();

        public void output(String pname)
        {
            Process[] ps = Process.GetProcesses();
            String title = "Process Name (ID)\tWorking Size\tThread\tHandle\tGDI\tUser\tSTime";
            Console.WriteLine(title);

            foreach (Process p in ps)
            {
                if (p.ProcessName.IndexOf(pname) != -1)
                {
                    String msg = p.ProcessName + "(" + p.Id.ToString() + ")";
                    msg += "\t" + p.WorkingSet64.ToString()
                         + "\t" + p.Threads.Count.ToString()
                         + "\t" + p.HandleCount.ToString()
                         + "\t" + u.GetGDIObjects((uint)p.Id).ToString()
                         + "\t" + u.GetUserObjects((uint)p.Id).ToString()
                         + "\t" + p.StartTime.ToShortTimeString();
                         
                    Console.WriteLine(msg+"\n");
                }



            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ProcessMonitor pm = new ProcessMonitor();
            if (args.Count() == 0)
            {
                Console.WriteLine("Usage : PerfLog 'Process Name' [-I=MilliSeconds (default = 2000)]");
                return;
            }

            int intval = 2000;
            String pname = "";

            foreach (String arg in args)
            {
                if (-1 < arg.IndexOf("-I="))
                {
                    intval = int.Parse(arg.Substring(3));
                }
                else
                {
                    pname = arg;
                }
            }

            if (pname == "")
            {
                Console.WriteLine("Process name should be given!! PerfLog exitted...");
                return;
            }

            while (true)
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString()); 
                pm.output(args[0]);
                System.Threading.Thread.Sleep(intval);
            }
        }
    }
}
