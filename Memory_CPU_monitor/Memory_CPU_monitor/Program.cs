using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Speech.Synthesis;          //added in references: Speech
using System.Diagnostics;       //contains performance counter  
using System.Threading;

namespace Memory_CPU_monitor
{
    class Program
    {

        static void Main(string[] args)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Speak("Welcome to the Monitor");              // make computer talk

            #region Perfomance counters
            PerformanceCounter perfCPUCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total"); //get current CPU load in percentage
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");                             //get current available memory in MB
            PerformanceCounter perfUpTime = new PerformanceCounter("System", "System Up Time");     //get number of seconds the system has been on
            #endregion

            while (true)
            {
                //get perfomance counter values.
                int CPUPercentage = (int)perfCPUCount.NextValue();
                int memAvailable =  (int)perfMemCount.NextValue();

                Console.WriteLine("CPU load:       : {0}%", CPUPercentage);       //perfCPUCount.NextValue() - return current number
                Console.WriteLine("Memory available: {0} MB", memAvailable);


                //speak to user if the values are in certain values.    
                if (CPUPercentage > 80)
                {
                    string cpuLoadVocalMessage = String.Format("the current CPU load is {0} percent", CPUPercentage);
                    synth.Speak(cpuLoadVocalMessage);
                }
                if (memAvailable < 500)
                {
                    string memAvailableVocalMessage = String.Format("the current available memory is {0} megabytes", memAvailable);
                    synth.Speak(memAvailableVocalMessage);
                }
               
                
                

                Thread.Sleep(1000);
            } //end of while
        }
    }
}
