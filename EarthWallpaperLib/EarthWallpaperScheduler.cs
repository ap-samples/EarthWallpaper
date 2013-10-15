using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EarthWallpaperLib
{
    public class EarthWallpaperScheduler
    {
        private readonly Timer ewTimer;
        private EarthWallpaper ew = new EarthWallpaper();
        private int UpdateIntervalMinutes { get; set; }

        public EarthWallpaperScheduler(int updateIntervalMinutes)
        {
            this.UpdateIntervalMinutes = updateIntervalMinutes;

            ewTimer = new Timer(1000 * 60 * updateIntervalMinutes);
            ewTimer.AutoReset = true;
            ewTimer.Enabled = true;
        }

        public void Start()
        {
            ew.UpdateWallpaper();
            ewTimer.Elapsed += ewTimer_Elapsed;
            ewTimer.Start();
        }

        void ewTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ew.UpdateWallpaper();
        }

        public void Stop()
        {
            ewTimer.Stop();
        }
    }
}
