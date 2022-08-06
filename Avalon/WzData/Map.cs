using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzDumper.WzData.Map
{
    public struct Tile
    {
        public Point position;
        public Assets.Sprite sprite;
    }

    public struct Layer
    {
        public List<Tile> tiles;
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
