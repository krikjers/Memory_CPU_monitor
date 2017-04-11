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
        private static  SpeechSynthesizer synth = new SpeechSynthesizer();

        static void Main(string[] args)
        {
            List<string> CPUmaxMessages = new List<string>();
            CPUmaxMessages.Add("Warning, CPU at 100 percent");
            CPUmaxMessages.Add("CPU is starting to melt");
            CPUmaxMessages.Add("Turn off your PC");

            Random rand = new Random();
         
            synth.Speak("Welcome to the Monitor");              // make computer talk

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
                   (int)    uptimeSpan.Seconds
                   );

            Speak(systemUptimeMessage, VoiceGender.Male, 7);       //tell user the uptime

            int speechSpeed = 1;

            while (true)
            {
                openWebsite("http://www.vg.no/");

                //get perfomance counter values.
                int CPUPercentage = (int)perfCPUCount.NextValue();
                int memAvailable =  (int)perfMemCount.NextValue();

                Console.WriteLine("CPU load:       : {0}%", CPUPercentage);       //perfCPUCount.NextValue() - return current number
                Console.WriteLine("Memory available: {0} MB", memAvailable);

                if (speechSpeed <5) { speechSpeed++; };           //gradually increase speech speed

                #region logic
                //speak to user if the values are in certain values.    
                if (CPUPercentage > 80)
                {                    
                    string cpuLoadVocalMessage = String.Format("the current CPU load is {0} percent", CPUPercentage);
                    Speak(cpuLoadVocalMessage, VoiceGender.Female, speechSpeed);
                }
                else if (CPUPercentage == 80)
                {
                    string cpuLoadVocalMessage = CPUmaxMessages[rand.Next(4)];
                    Speak(cpuLoadVocalMessage, VoiceGender.Female, speechSpeed);
                }
                if (memAvailable > 500)
                {                    
                    string memAvailableVocalMessage = String.Format("the current available memory is {0} megabytes", memAvailable);
                    Speak(memAvailableVocalMessage, VoiceGender.Male, speechSpeed);
                }
                #endregion

                Thread.Sleep(1000);     //delay for 1 sec
            } //end of while
        } // end main


        /// <summary>
        /// speak in selected voice 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="gender"></param>
        public static void Speak(string msg, VoiceGender gender)                //static keyword: we dont need an object of program class to call speak()
        {
            //synth.Rate = 1;
            synth.SelectVoiceByHints(gender);       //bug with gender....
            synth.Speak(msg);
        }
        /// <summary>
        /// speak in selected voice with selected speed
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="gender"></param>
        public static void Speak(string msg, VoiceGender gender, int talkRate)                //overloading Speak()
        {
            synth.Rate = talkRate;
            Speak(msg, gender);
        }

        public static void openWebsite(string URL)
        {
            //start a process 
            Process process1 = new Process();
            process1.StartInfo.FileName = "chrome.exe";
            process1.StartInfo.Arguments = URL;
            process1.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process1.Start();
        }

    }
}
