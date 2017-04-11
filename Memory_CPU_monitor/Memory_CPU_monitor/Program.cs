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
            //synth.Speak("Welcome to the Monitor");              // make computer talk

            #region Perfomance counters
            PerformanceCounter perfCPUCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total"); //get current CPU load in percentage
            perfCPUCount.NextValue();   //initialize objct value
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");                             //get current available memory in MB
            perfMemCount.NextValue();
            PerformanceCounter perfUpTime = new PerformanceCounter("System", "System Up Time");     //get number of seconds the system has been on
            perfUpTime.NextValue();
            #endregion

            TimeSpan uptimeSpan = TimeSpan.FromSeconds(perfUpTime.NextValue());     //make timespan object from seconds, to get hours, mins, sec
            string systemUptimeMessage = string.Format("current uptime is {0} days, {1} hours, {2} minutes, {3} seconds",
                   (int)uptimeSpan.TotalDays,           //casting "trims off" desimal part
                   (int)uptimeSpan.Hours,
                   (int)uptimeSpan.Minutes,
                   (int)uptimeSpan.Seconds
                   );

            synth.Speak(systemUptimeMessage);       //tell user the uptime

            while (true)
            {
                //get perfomance counter values.
                int CPUPercentage = (int)perfCPUCount.NextValue();
                int memAvailable =  (int)perfMemCount.NextValue();

                Console.WriteLine("CPU load:       : {0}%", CPUPercentage);       //perfCPUCount.NextValue() - return current number
                Console.WriteLine("Memory available: {0} MB", memAvailable);


                //speak to user if the values are in certain values.    
                if (CPUPercentage < 80)
                {
                    //synth.SelectVoiceByHints(VoiceGender.Male);
                    string cpuLoadVocalMessage = String.Format("the current CPU load is {0} percent", CPUPercentage);
                    synth.Speak(cpuLoadVocalMessage);
                }
                if (memAvailable > 500)
                {
                    //synth.SelectVoiceByHints(VoiceGender.Male);
                    string memAvailableVocalMessage = String.Format("the current available memory is {0} megabytes", memAvailable);
                    synth.Speak(memAvailableVocalMessage);
                }
               
                
                

                Thread.Sleep(1000);
            } //end of while
        }
    }
}
