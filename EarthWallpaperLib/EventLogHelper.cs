using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthWallpaperLib
{
    class EventLogHelper
    {
        const string APP_LOG_NAME = "EarthWallpaper";
        EventLog eLog = new EventLog();

        public EventLogHelper()
        {
        }

        public void Error(string msg)
        {
            EventLog.WriteEntry("Application", msg, EventLogEntryType.Error);
        }
    }
}
