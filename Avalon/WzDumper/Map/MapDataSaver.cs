using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HaCreator.MapSimulator.MapSimulator;

namespace WzDumper.Map
{
    public static class MapDataSaver
    {
        public static void SaveTo(this WzData.Assets assets, string assetsDirectory)
        {
            if (!Directory.Exists(assetsDirectory))
            {
                throw new DirectoryNotFoundException($"{assetsDirectory} directory doesn't exist or isn't a directory");
            }

            Console.WriteLine("Saving bitmaps...");
            foreach (var (path, bitmap) in assets.bitmaps.Select(pair => (pair.Key, pair.Value)))
            {
                var filePath = $"{Path.Combine(assetsDirectory, path)}.png";
                if (!File.Exists(filePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    bitmap.Save(filePath, ImageFormat.Png);

                    Console.WriteLine($"  Saved \"{path}.png\"");
                }
            }
            Console.WriteLine("Done!");
        }
    }
}
