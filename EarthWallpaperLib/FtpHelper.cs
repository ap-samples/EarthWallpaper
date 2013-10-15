using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

using System.Diagnostics;

namespace EarthWallpaperLib
{
    class FtpHelper
    {
        private string Username;
        private string Password;

        public FtpHelper(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public bool URIAccessible(string serverURI)
        {
            FtpWebRequest req = GetFtpWebRequest(serverURI,WebRequestMethods.Ftp.ListDirectory);

            try
            {
                using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse()) { }
            }
            catch (WebException ex)
            {
                if(ex.Response != null)
                    System.Diagnostics.Debug.WriteLine("Error:"+((FtpWebResponse)ex.Response).StatusCode);
                return false;
            }

            return true;
        }

        public bool DownloadFile(string fileURI, string destFilePath)
        {
            FtpWebRequest req = GetFtpWebRequest(fileURI, WebRequestMethods.Ftp.DownloadFile);

            using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
            {
                using (BinaryReader sr = new BinaryReader(resp.GetResponseStream()))
                {
                    byte[] buffer = new byte[4096];

                    using (BinaryWriter sw = new BinaryWriter(File.OpenWrite(destFilePath)))
                    {
                        int count;
                        while ((count = sr.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            sw.Write(buffer, 0, count);
                        }
                        sw.Flush();
                    }
                }
            }

            return true;
        }

        public string ListItemsInFolder(string serverURI)
        {
            //Debug.WriteLine("Server URI:" + serverURI);
            FtpWebRequest req = GetFtpWebRequest(serverURI, WebRequestMethods.Ftp.ListDirectory);
            
            using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
            {
                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        private FtpWebRequest GetFtpWebRequest(string URI, string method)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(URI);
            req.Method = method;
            req.Credentials = new NetworkCredential(this.Username, this.Password);
            req.KeepAlive = true;

            return req;
        }
    }
}
