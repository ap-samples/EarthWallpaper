using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthWallpaperLib;

namespace EarthWallpaperCons
{
    class Program
    {
        static void Main(string[] args)
        {
            new EarthWallpaper().UpdateWallpaper();
            Console.WriteLine("Wallaper updated");

            Console.ReadLine();
        }
    }
}
