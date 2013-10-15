using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using EarthWallpaperLib;

namespace EarthWallpaperAutoUpdate
{
    public partial class EarthWallpaperAutoUpdate : ServiceBase
    {
        private EarthWallpaperScheduler ewc;

        public EarthWallpaperAutoUpdate()
        {
            InitializeComponent();

            ewc = new EarthWallpaperScheduler(Int32.Parse(ConfigurationManager.AppSettings["UpdateIntervalMinutes"]));
        }

        protected override void OnStart(string[] args)
        {
            ewc.Start();
        }

        protected override void OnStop()
        {
            ewc.Stop();
        }
    }
}
