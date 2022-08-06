using HaCreator.MapSimulator.Objects.FieldObject;
using HaSharedLibrary.Render.DX;
using HaSharedLibrary.Util;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using MapleLib.WzLib.WzStructure.Data;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace HaCreator.MapSimulator
{
    public partial class MapSimulator
    {
        public (WzDumper.WzData.Map.MapData, Dictionary<string, System.Drawing.Bitmap>) DumpMap()
        {
            var mapData = new WzDumper.WzData.Map.MapData() {
                mapSize = new WzDumper.WzData.Size() {
                    width = vr_fieldBoundary.Width - vr_fieldBoundary.X,
                    height = vr_fieldBoundary.Height - vr_fieldBoundary.Y
                }
            };

            var bitmaps = new Dictionary<string, Bitmap>();

            Console.WriteLine($"Map size: {{{mapData.mapSize.width} {mapData.mapSize.height}}}");
            Console.WriteLine($"Dumping tiles...");
            DumpTiles(ref mapData, ref bitmaps);

            return (mapData, bitmaps);
        }

        private void DumpTiles(ref WzDumper.WzData.Map.MapData mapData, ref Dictionary<string, Bitmap> bitmaps)
        {
            var layers = new List<WzDumper.WzData.Map.Layer>();
            foreach (var (layer, layer_index) in mapObjects.Select((value, index) => (value, index)))
            {
                Console.Write($"  Dumping layer {layer_index}... ");

                var layerData = new WzDumper.WzData.Map.Layer() {
                    tiles = new List<WzDumper.WzData.Map.Tile>()
                };

                foreach (var tile in layer)
                {
                    if (tile.source is WzImageProperty image)
                    {
                        var (sprite, tileBitmaps) = MapSimulatorLoader.LoadSprite(image);

                        int centerX = mapBoard.CenterPoint.X;
                        int centerY = mapBoard.CenterPoint.Y;
                        int x = centerX + tile.Position.X - vr_fieldBoundary.X;
                        int y = centerY + tile.Position.Y - vr_fieldBoundary.Y;

                        var tileData = new WzDumper.WzData.Map.Tile() {
                            position = new WzDumper.WzData.Point() {
                                x = x,
                                y = y
                            },
                            sprite = sprite
                        };

                        layerData.tiles.Add(tileData);
                        foreach (var (path, bitmap) in tileBitmaps.Select(pair => (pair.Key, pair.Value)))
                        {
                            bitmaps[path] = bitmap;
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }

                layers.Add(layerData);
                Console.WriteLine($"Done!");
            }

            mapData.layers = layers;
        }
    }
}
