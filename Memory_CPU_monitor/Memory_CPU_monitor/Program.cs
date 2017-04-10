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
            //get CPU load in percentage
            PerformanceCounter perfCPUCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

            while(true)
            {
                Thread.Sleep(1000);
                Console.WriteLine("CPU load: {0}", perfCPUCount.NextValue());       //perfCPUCount.NextValue() - return current number
            }
        }
    }
}
