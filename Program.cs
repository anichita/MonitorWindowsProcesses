using System;
using System.Diagnostics;
using System.Threading;

namespace MonitorWindowsProcesses
{
    class Program
    {
        public static void KillProcess(string processName, double maximumLifetime)
        {  
            var processByName = Process.GetProcessesByName(processName);
          
            foreach (var process in processByName)
            {                
                var lifetime = (DateTime.Now - process.StartTime).TotalMinutes;
                if (lifetime > maximumLifetime) 
                {
                    Console.WriteLine($"Process: {process.ProcessName} started at {process.StartTime} was closed at {DateTime.Now}.");
                    process.Kill();
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter Process Name:");
            var processName = Console.ReadLine();

            Console.WriteLine("Enter Maximum Lifetime:");
            var maximumLifetime = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter Monitoring Frequency:");
            var monitoringFrequency = Convert.ToInt32(Console.ReadLine());

            var startTime = DateTime.Now;
            var frequency = 0;         

            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
                if ((DateTime.Now - startTime).Milliseconds == frequency * 60 * 1000)
                {
                    KillProcess(processName, maximumLifetime);
                    frequency += monitoringFrequency;
                }
               
            }
        }
    }
}
