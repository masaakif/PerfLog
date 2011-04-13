using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace PerfLog
{
    public class ProcessMonitor
    {
        WindowsAPIs.Utils u = new WindowsAPIs.Utils();
        PerformanceCounter pcLogicalDiskCount;
        PerformanceCounter pcLogicalDiskTime;
        PerformanceCounter pcMemoryAvailable;
        PerformanceCounter pcPageSec;
        PerformanceCounter pcProcessorTime;

        public ProcessMonitor()
        {
            pcLogicalDiskCount = new PerformanceCounter("LogicalDisk", "% Free Space", "C:");
            pcLogicalDiskTime = new PerformanceCounter("LogicalDisk", "% Disk Time", "C:");
            pcMemoryAvailable = new PerformanceCounter("Memory", "Available Bytes");
            pcPageSec = new PerformanceCounter("Memory", "Pages/sec");
            pcProcessorTime = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public String getProcessPerfLog(String pname)
        {
            Process[] ps = Process.GetProcesses();
            //String title = "Process Name (ID)\tWorking Size\tThread\tHandle\tGDI\tUser\tSTime\n";
            String msg = "";
            //Console.WriteLine(title);

            foreach (Process p in ps)
            {
                if (p.ProcessName.IndexOf(pname) != -1)
                {
                    msg += "\t\tProcess Name(ID) = " + p.ProcessName + "(" + p.Id.ToString() + ")" + "\n"
                        + "\t\tWorking Size = " + p.WorkingSet64.ToString() + "\n"
                        + "\t\tThread = " + p.Threads.Count.ToString() + "\n"
                        + "\t\tHandle = " + p.HandleCount.ToString() + "\n"
                        + "\t\tGDI Objects = " + u.GetGDIObjects((uint)p.Id).ToString() + "\n"
                        + "\t\tUser Objects = " + u.GetUserObjects((uint)p.Id).ToString() + "\n"
                        + "\t\tStart Time = " + p.StartTime.ToShortTimeString() + "\n";
                }
            }

            if (0 < ps.Count())
            {
                return msg;
            }

            return "";
        }

        public String getPCPerformance()
        {
            //String title = "Disk %Free\tDisk %Time\tAvail Bytes\tPages/sec\t%ProcTime\n";
            String msg = "\tDisk % Free = " + pcLogicalDiskCount.NextValue().ToString() + "\n"
                       + "\t\tDisk % Time = " + pcLogicalDiskTime.NextValue().ToString() + "\n"
                       + "\t\tAvail Bytes = " + (pcMemoryAvailable.NextValue() / 1024 / 1024).ToString() + "MB" + "\n"
                       + "\t\tPages/sec = " + pcPageSec.NextValue().ToString() + "\n"
                       + "\t\tProcTime = " + pcProcessorTime.NextValue().ToString() + "\n";

            return msg;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ProcessMonitor pm = new ProcessMonitor();
            if (args.Count() == 0)
            {
                Console.WriteLine("Usage : PerfLog 'Process Name' [-I=MilliSeconds (default = 2000)] [-O=Output file name]");
                return;
            }

            int intval = 2000;
            String pname = "";
            String ofile = "";

            foreach (String arg in args)
            {
                if (-1 < arg.IndexOf("-I="))
                {
                    intval = int.Parse(arg.Substring(3));
                }
                else if (-1 < arg.IndexOf("-O"))
                {
                    ofile = arg.Substring(3);
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
                String msg = "===============================================================================\n";
                msg += DateTime.Now.ToLongTimeString(); 
                msg += pm.getPCPerformance();
                msg += "---------------------\n";
                msg += pm.getProcessPerfLog(args[0]);

                Console.WriteLine(msg);
                

                if (ofile != "")
                {
                    File.AppendAllText(ofile, msg);
                }

                if (intval <= 0)
                {
                    break;
                }

                System.Threading.Thread.Sleep(intval);
            }
        }
    }
}
