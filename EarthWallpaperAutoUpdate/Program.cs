using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using EarthWallpaperLib;
using System.Configuration;

namespace EarthWallpaperAutoUpdate
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Contains("-console"))
            {
                EarthWallpaperScheduler ewc = new EarthWallpaperScheduler(Int32.Parse(ConfigurationManager.AppSettings["UpdateIntervalMinutes"]));
                Console.WriteLine("Starting service...");

                ewc.Start();
                Console.WriteLine("Press 'q' to quit");
                while (Console.ReadLine() != "q") { }
                ewc.Stop();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new EarthWallpaperAutoUpdate() 
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
