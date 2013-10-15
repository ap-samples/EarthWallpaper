using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Net;
using System.Diagnostics;

namespace EarthWallpaperLib
{
    public class EarthWallpaper
    {
        private readonly string FOLDER_PATH = Path.GetTempPath() + @"/EarthWallpaper/";
        private readonly int RetriesWithEarlierDateCount;
        private readonly int KeepLatestWallpapersCount;
        private const string FTP_ADDR_BASE = @"ftp://ftp.ntsomz.ru/";
        private const string USERNAME = "electro";
        private const string PASSWORD = "electro";
        private const string FILENAME_DATE_FORMAT = "yyyy_MM_dd_";

        private readonly FtpHelper fh = new FtpHelper(username: USERNAME, password: PASSWORD);
        private readonly EventLogHelper elh = new EventLogHelper();

        public EarthWallpaper(int retriesWithEarlierDateCount = 5, int keepLatestWallpapersCount = -1)
        {
            if (!Directory.Exists(FOLDER_PATH))
                Directory.CreateDirectory(FOLDER_PATH);

            this.RetriesWithEarlierDateCount = retriesWithEarlierDateCount;
            this.KeepLatestWallpapersCount = keepLatestWallpapersCount;
        }

        public void UpdateWallpaper()
        {
            DateTime currTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc).AddHours(2);

            try
            {
                GetLatestWallpaper(currTime);
            }
            catch (WebException we)
            {
                elh.Error("Network error:" + we.Message + "\n" + we.StackTrace);
            }
            catch (Exception e)
            {
                elh.Error("Error:" + e.Message + "\n" + e.StackTrace);
            }
        }

        private void GetLatestWallpaper(DateTime currTime, int retryIndex = 0)
        {
            if (retryIndex > RetriesWithEarlierDateCount)
                return;

            string currMonth = currTime.ToString("MMMM", CultureInfo.InvariantCulture);
            string currDate = currTime.ToString("dd");
            string lastPhotoHHmm = "";
            bool isFileAccessible = false;
            
            string serverURIMainPart = GetURIMainPart(FTP_ADDR_BASE, currTime.Year.ToString(), currMonth, currDate);
            string folders = fh.ListItemsInFolder(serverURIMainPart);

            List<int> foldersNumericList = folders.Split('\n').Where(i => !String.IsNullOrEmpty(i)).Select(i => Int32.Parse(i)).ToList();
            string serverURI = "";

            while (!isFileAccessible && foldersNumericList.Count > 1)
            {
                lastPhotoHHmm = foldersNumericList.Max().ToString().PadLeft(4, '0');

                serverURI = serverURIMainPart + lastPhotoHHmm + @"/" + GetFTPFileName(currTime, lastPhotoHHmm);
                isFileAccessible = fh.URIAccessible(serverURI);
                foldersNumericList.Remove(Int32.Parse(lastPhotoHHmm));
            }

            if (!isFileAccessible)
            {
                GetLatestWallpaper(currTime.AddDays(-1), retryIndex++);
            }

            string destFilePath = FOLDER_PATH + currTime.ToString(FILENAME_DATE_FORMAT) + lastPhotoHHmm + ".jpg";
            if (File.Exists(destFilePath))
            {
                Debug.WriteLine("Last available wallpaper " + currTime.ToString("yyyy.MM.dd hh:mm") + " already exists");
                return;
            }

            fh.DownloadFile(serverURI, destFilePath);
            new WallpaperHelper().Set(destFilePath);            
        }

        private string GetFTPFileName(DateTime timeOfPhoto, string photoHHmm)
        {
            return timeOfPhoto.ToString("yyMMdd_") + photoHHmm + "_RGB.jpg";
        }

        private string GetURIMainPart(string addressBase, string year, string month, string date)
        {
            return addressBase + year + @"/" + month + @"/" + date + @"/";
        }
    }
}
    