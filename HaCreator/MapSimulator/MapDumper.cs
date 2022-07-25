using HaCreator.MapSimulator.Objects.FieldObject;
using HaSharedLibrary.Render.DX;
using HaSharedLibrary.Util;
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

namespace HaCreator.MapSimulator
{
    public partial class MapSimulator
    {
        public MapDumper.MapData DumpMap()
        {
            var mapData = new MapDumper.MapData() {
                mapSize = new MapDumper.Size() {
                    width = vr_fieldBoundary.Width - vr_fieldBoundary.X,
                    height = vr_fieldBoundary.Height - vr_fieldBoundary.Y
                },
                assets = new MapDumper.Assets() {
                    bitmaps = new Dictionary<string, Bitmap>()
                }
            };

            Console.WriteLine($"Map size: {{{mapData.mapSize.width} {mapData.mapSize.height}}}");
            Console.WriteLine($"Dumping tiles...");
            DumpTiles(ref mapData);

            return mapData;
        }

        private void DumpTiles(ref MapDumper.MapData mapData)
        {
            var layers = new List<MapDumper.Layer>();
            foreach (var (layer, layer_index) in mapObjects.Select((value, index) => (value, index)))
            {
                Console.Write($"  Dumping layer {layer_index}... ");

                var layerData = new MapDumper.Layer() {
                    tiles = new List<MapDumper.Tile>()
                };

                foreach (var obj in layer)
                {
                    var frames = new List<MapDumper.Frame>();
                    if (obj.frames.Count == 0)
                    {
                        throw new NotImplementedException();
                    }
                    foreach (var frame in obj.frames)
                    {
                        int width = frame.Width;
                        int height = frame.Height;

                        int centerX = mapBoard.CenterPoint.X;
                        int centerY = mapBoard.CenterPoint.Y;
                        int x = centerX + frame.X - vr_fieldBoundary.X;
                        int y = centerY + frame.Y - vr_fieldBoundary.Y;

                        var frameData = new MapDumper.Frame() {
                            durationMs = (frame.Delay * 1000) / 60, // convert to milliseconds, devide by 60 fps
                            position = new MapDumper.Point() {
                                x = x,
                                y = y
                            }
                        };

                        if (frame.Source is WzCanvasProperty canvas)
                        {
                            frameData.bitmapPath = canvas.FullPath;

                            if (!mapData.assets.bitmaps.ContainsKey(frameData.bitmapPath))
                            {
                                mapData.assets.bitmaps[frameData.bitmapPath] = canvas.GetLinkedWzCanvasBitmap();
                            }
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }

                        frames.Add(frameData);
                    }

                    var tileData = new MapDumper.Tile() {
                        frames = frames
                    };

                    layerData.tiles.Add(tileData);
                }

                layers.Add(layerData);
                Console.WriteLine($"Done!");
            }

            mapData.layers = layers;
        }
    }
}
