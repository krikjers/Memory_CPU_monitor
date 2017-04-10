using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;       //contains performance counter  
using System.Threading;

namespace Memory_CPU_monitor
{
    class Program
    {

        static void Main(string[] args)
        {
            
            PerformanceCounter perfCPUCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total"); //get current CPU load in percentage
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");                             //get current available memory use in MB
            while (true)
            {
                Thread.Sleep(1000); 
                Console.WriteLine("CPU load: {0}%", perfCPUCount.NextValue());       //perfCPUCount.NextValue() - return current number
                Console.WriteLine("Memory available: {0} MB", perfCPUCount.NextValue());
            }
        }
    }
}
