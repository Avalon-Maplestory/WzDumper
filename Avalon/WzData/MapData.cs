using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzDumper.WzData
{
    public struct Frame
    {
        public string bitmapPath;
        public int durationMs;
        public Point position;
    }

    public struct Tile
    {
        public List<Frame> frames;
    }

    public struct Layer
    {
        public List<Tile> tiles;
    }

    public struct Assets
    {
        public Dictionary<string, Bitmap> bitmaps; // BitmapPath -> Bitmap
    }

    public struct MapData
    {
        public Size mapSize;
        public List<Layer> layers;
    }

    public struct AvailableMap
    {
        public int mapId;
        public string mapStreetName;
        public string mapName;
    }

    public struct AvailableMaps
    {
        public List<AvailableMap> maps;
    }
}
