using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HaCreator.MapSimulator.MapSimulator;

namespace WzDumperCS
{
    public static class MapDataSaver
    {
        public static string ToJson(this WzDumper.MapData mapData)
        {
            return JsonConvert.SerializeObject(mapData, Formatting.Indented);
        }

        public static void SaveMapAssets(this WzDumper.MapData mapData, string assetsDirectory)
        {
            if (!Directory.Exists(assetsDirectory))
            {
                throw new DirectoryNotFoundException($"{assetsDirectory} directory doesn't exist or isn't a directory");
            }

            Console.WriteLine("Saving bitmaps...");
            foreach (var (path, bitmap) in mapData.assets.bitmaps.Select(pair => (pair.Key, pair.Value)))
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
