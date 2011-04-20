using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace PerfLog
{
    public class PCInfo
    {
        string[] architecture = { "x86", "MIPS", "Alpha", "PowerPC", "4", "5", "Itanium-based systems", "7", "8", "x64" };
        Dictionary<int, String> family = new Dictionary<int, string>()
        {   {1,"Other"},{2,"Unknown"},{3,"8086"},{4,"80286"},{5,"80386"},{6,"80486"},{7,"8087"},{8,"80287"},{9,"80387"},{10,"80487"},{11,"Pentium(R)brand"},
            {12,"Pentium(R)Pro"},{13,"Pentium(R)II"},{14,"Pentium(R)processorwithMMX(TM)technology"},{15,"Celeron(TM)"},{16,"Pentium(R)IIXeon(TM)"},{17,"Pentium(R)III"},
            {18,"M1Family"},{19,"M2Family"},{20,"Intel(R)Celeron(R)Mprocessor"},{21,"Intel(R)Pentium(R)4HTprocessor"},{24,"K5Family"},{25,"K6Family"},{26,"K6-2"},{27,"K6-3"},
            {28,"AMDAthlon(TM)ProcessorFamily"},{29,"AMD(R)Duron(TM)Processor"},{30,"AMD29000Family"},{31,"K6-2+"},{32,"PowerPCFamily"},{33,"PowerPC601"},{34,"PowerPC603"},
            {35,"PowerPC603+"},{36,"PowerPC604"},{37,"PowerPC620"},{38,"PowerPCX704"},{39,"PowerPC750"},{40,"Intel(R)Core(TM)Duoprocessor"},{41,"Intel(R)Core(TM)Duomobileprocessor"},
            {42,"Intel(R)Core(TM)Solomobileprocessor"},{43,"Intel(R)Atom(TM)processor"},{48,"AlphaFamily"},{49,"Alpha21064"},{50,"Alpha21066"},{51,"Alpha21164"},{52,"Alpha21164PC"},
            {53,"Alpha21164a"},{54,"Alpha21264"},{55,"Alpha21364"},{56,"AMDTurion(TM)IIUltraDual-CoreMobileMProcessorFamily"},{57,"AMDTurion(TM)IIDual-CoreMobileMProcessorFamily"},
            {58,"AMDAthlon(TM)IIDual-CoreMobileMProcessorFamily"},{59,"AMDOpteron(TM)6100SeriesProcessor"},{60,"AMDOpteron(TM)4100SeriesProcessor"},{64,"MIPSFamily"},{65,"MIPSR4000"},
            {66,"MIPSR4200"},{67,"MIPSR4400"},{68,"MIPSR4600"},{69,"MIPSR10000"},{80,"SPARCFamily"},{81,"SuperSPARC"},{82,"microSPARCII"},{83,"microSPARCIIep"},{84,"UltraSPARC"},
            {85,"UltraSPARCII"},{86,"UltraSPARCIIi"},{87,"UltraSPARCIII"},{88,"UltraSPARCIIIi"},{96,"68040"},{97,"68xxxFamily"},{98,"68000"},{99,"68010"},{100,"68020"},{101,"68030"},
            {112,"HobbitFamily"},{120,"Crusoe(TM)TM5000Family"},{121,"Crusoe(TM)TM3000Family"},{122,"Efficeon(TM)TM8000Family"},{128,"Weitek"},{129,"Reserved"},{130,"Itanium(TM)Processor"},
            {131,"AMDAthlon(TM)64ProcessorFamily"},{132,"AMDOpteron(TM)ProcessorFamily"},{133,"AMDSempron(TM)ProcessorFamily"},{134,"AMDTurion(TM)64MobileTechnology"},{135,"Dual-CoreAMDOpteron(TM)ProcessorFamily"},
            {136,"AMDAthlon(TM)64X2Dual-CoreProcessorFamily"},{137,"AMDTurion(TM)64X2MobileTechnology"},{138,"Quad-CoreAMDOpteron(TM)ProcessorFamily"},{139,"Third-GenerationAMDOpteron(TM)ProcessorFamily"},
            {140,"AMDPhenom(TM)FXQuad-CoreProcessorFamily"},{141,"AMDPhenom(TM)X4Quad-CoreProcessorFamily"},{142,"AMDPhenom(TM)X2Dual-CoreProcessorFamily"},{143,"AMDAthlon(TM)X2Dual-CoreProcessorFamily"},
            {144,"PA-RISCFamily"},{145,"PA-RISC8500"},{146,"PA-RISC8000"},{147,"PA-RISC7300LC"},{148,"PA-RISC7200"},{149,"PA-RISC7100LC"},{150,"PA-RISC7100"},{160,"V30Family"},
            {161,"Quad-CoreIntel(R)Xeon(R)processor3200Series"},{162,"Dual-CoreIntel(R)Xeon(R)processor3000Series"},{163,"Quad-CoreIntel(R)Xeon(R)processor5300Series"},
            {164,"Dual-CoreIntel(R)Xeon(R)processor5100Series"},{165,"Dual-CoreIntel(R)Xeon(R)processor5000Series"},{166,"Dual-CoreIntel(R)Xeon(R)processorLV"},
            {167,"Dual-CoreIntel(R)Xeon(R)processorULV"},{168,"Dual-CoreIntel(R)Xeon(R)processor7100Series"},{169,"Quad-CoreIntel(R)Xeon(R)processor5400Series"},
            {170,"Quad-CoreIntel(R)Xeon(R)processor"},{171,"Dual-CoreIntel(R)Xeon(R)processor5200Series"},{172,"Dual-CoreIntel(R)Xeon(R)processor7200Series"},
            {173,"Quad-CoreIntel(R)Xeon(R)processor7300Series"},{174,"Quad-CoreIntel(R)Xeon(R)processor7400Series"},{175,"Multi-CoreIntel(R)Xeon(R)processor7400Series"},
            {176,"Pentium(R)IIIXeon(TM)"},{177,"Pentium(R)IIIProcessorwithIntel(R)SpeedStep(TM)Technology"},{178,"Pentium(R)4"},{179,"Intel(R)Xeon(TM)"},{180,"AS400Family"},
            {181,"Intel(R)Xeon(TM)processorMP"},{182,"AMDAthlon(TM)XPFamily"},{183,"AMDAthlon(TM)MPFamily"},{184,"Intel(R)Itanium(R)2"},{185,"Intel(R)Pentium(R)Mprocessor"},
            {186,"Intel(R)Celeron(R)Dprocessor"},{187,"Intel(R)Pentium(R)Dprocessor"},{188,"Intel(R)Pentium(R)ProcessorExtremeEdition"},{189,"Intel(R)Core(TM)SoloProcessor"},
            {190,"K7"},{191,"Intel(R)Core(TM)2DuoProcessor"},{192,"Intel(R)Core(TM)2Soloprocessor"},{193,"Intel(R)Core(TM)2Extremeprocessor"},{194,"Intel(R)Core(TM)2Quadprocessor"},
            {195,"Intel(R)Core(TM)2Extrememobileprocessor"},{196,"Intel(R)Core(TM)2Duomobileprocessor"},{197,"Intel(R)Core(TM)2Solomobileprocessor"},{198,"Intel(R)Core(TM)i7processor"},
            {199,"Dual-CoreIntel(R)Celeron(R)Processor"},{200,"S/390andzSeriesFamily"},{201,"ESA/390G4"},{202,"ESA/390G5"},{203,"ESA/390G6"},{204,"z/Architecturbase"},{205,"Intel(R)Core(TM)i5processor"},
            {206,"Intel(R)Core(TM)i3processor"},{210,"VIAC7(TM)-MProcessorFamily"},{211,"VIAC7(TM)-DProcessorFamily"},{212,"VIAC7(TM)ProcessorFamily"},{213,"VIAEden(TM)ProcessorFamily"},
            {214,"Multi-CoreIntel(R)Xeon(R)processor"},{215,"Dual-CoreIntel(R)Xeon(R)processor3xxxSeries"},{216,"Quad-CoreIntel(R)Xeon(R)processor3xxxSeries"},{217,"VIANano(TM)ProcessorFamily"},
            {218,"Dual-CoreIntel(R)Xeon(R)processor5xxxSeries"},{219,"Quad-CoreIntel(R)Xeon(R)processor5xxxSeries"},{221,"Dual-CoreIntel(R)Xeon(R)processor7xxxSeries"},
            {222,"Quad-CoreIntel(R)Xeon(R)processor7xxxSeries"},{223,"Multi-CoreIntel(R)Xeon(R)processor7xxxSeries"},{224,"Multi-CoreIntel(R)Xeon(R)processor3400Series"},{230,"EmbeddedAMDOpteron(TM)Quad-CoreProcessorFamily"},
            {231,"AMDPhenom(TM)Triple-CoreProcessorFamily"},{232,"AMDTurion(TM)UltraDual-CoreMobileProcessorFamily"},{233,"AMDTurion(TM)Dual-CoreMobileProcessorFamily"},{234,"AMDAthlon(TM)Dual-CoreProcessorFamily"},
            {235,"AMDSempron(TM)SIProcessorFamily"},{236,"AMDPhenom(TM)IIProcessorFamily"},{237,"AMDAthlon(TM)IIProcessorFamily"},{238,"Six-CoreAMDOpteron(TM)ProcessorFamily"},
            {239,"AMDSempron(TM)MProcessorFamily"},{250,"i860"},{251,"i960"},{254,"Reserved(SMBIOSExtension)"},{255,"Reserved(Un-initializedFlashContent-Lo)"},{260,"SH-3"},
            {261,"SH-4"},{280,"ARM"},{281,"StrongARM"},{300,"6x86"},{301,"MediaGX"},{302,"MII"},{320,"WinChip"},{350,"DSP"},{500,"VideoProcessor"},{65534,"Reserved(ForFutureSpecialPurposeAssignment)"},
            {65535, "Reserved(Un-initializedFlashContent-Hi)"},
        };

        ManagementClass mc;

        public string showSpec()
        {
            String msg = "";

            mc = new ManagementClass("Win32_OperatingSystem");
            foreach (ManagementObject mo in mc.GetInstances())
            {

                msg += String.Format("Phisical Memory = {0} MBytes\n", (ulong)(mo["TotalVisibleMemorySize"]) / 1024);
                msg += String.Format("Locale = {0} {1}\n", mo["Locale"], mo["LocalDateTime"]);
            }

            mc = new ManagementClass("Win32_Processor");
            foreach (ManagementObject mo in mc.GetInstances())
            {
                msg += "Architecture = " + architecture[(ushort)mo["Architecture"]] + " ";
                msg += String.Format("({0} Cores)\n", mo["NumberOfCores"]);
                msg += String.Format("Family = {0}\n", family[(ushort)mo["Family"]]);
                msg += "Caption = " + mo["Caption"] + "\n";
                msg += String.Format("Clock = {0} MHz\n", mo["CurrentClockSpeed"]);
                msg += String.Format("DataWidth = {0} bits", mo["DataWidth"]);

            }

            return msg;
        }

        public string showDriveInfo()
        {
            float gb = 1024f * 1024f * 1024f;
            String msg = "";
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady) {
                    msg += String.Format("Drive {0}'{1}' ({2}) : free {3} / {4} GB\n", d.Name, d.VolumeLabel, d.DriveType,(float)d.AvailableFreeSpace/gb, (float)d.TotalSize/gb);
                }
            }
            return msg;
        }
    }

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
            PCInfo pi = new PCInfo();
            Console.WriteLine(pi.showSpec());
            Console.WriteLine("--------------------");
            Console.Write(pi.showDriveInfo());

            
            if (args.Count() == 0)
            {
                Console.WriteLine("Usage : PerfLog 'Process Name' [-I=MilliSeconds (default = 2000)] [-O=Output file name]");
                return;
            }

            ProcessMonitor pm = new ProcessMonitor();

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

            int cnt = 0;
            while (true)
            {
                String ppl = pm.getProcessPerfLog(args[0]);
                

                if (1 < ppl.Length)
                {
                    if (0 < cnt)
                    {
                        Console.WriteLine("");
                        cnt = 0;
                    }

                    String msg = "===============================================================================\n"
                               + DateTime.Now.ToLongTimeString()
                               + pm.getPCPerformance()
                               + "---------------------\n"
                               + ppl;

                    Console.WriteLine(msg);


                    if (ofile != "")
                    {
                        File.AppendAllText(ofile, msg);
                    }

                    if (intval <= 0)
                    {
                        break;
                    }
                }
                else
                {
                    if (cnt == 0)
                    {
                        Console.Write("Awaiting");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                    cnt++;
                }


                System.Threading.Thread.Sleep(intval);
            }
        }
    }
}
