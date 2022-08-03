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
        public (WzDumper.WzData.MapData, WzDumper.WzData.Assets) DumpMap()
        {
            var mapData = new WzDumper.WzData.MapData() {
                mapSize = new WzDumper.WzData.Size() {
                    width = vr_fieldBoundary.Width - vr_fieldBoundary.X,
                    height = vr_fieldBoundary.Height - vr_fieldBoundary.Y
                }
            };

            var assets = new WzDumper.WzData.Assets() {
                bitmaps = new Dictionary<string, Bitmap>()
            };

            Console.WriteLine($"Map size: {{{mapData.mapSize.width} {mapData.mapSize.height}}}");
            Console.WriteLine($"Dumping tiles...");
            DumpTiles(ref mapData, ref assets);

            return (mapData, assets);
        }

        private void DumpTiles(ref WzDumper.WzData.MapData mapData, ref WzDumper.WzData.Assets assets)
        {
            var layers = new List<WzDumper.WzData.Layer>();
            foreach (var (layer, layer_index) in mapObjects.Select((value, index) => (value, index)))
            {
                Console.Write($"  Dumping layer {layer_index}... ");

                var layerData = new WzDumper.WzData.Layer() {
                    tiles = new List<WzDumper.WzData.Tile>()
                };

                foreach (var obj in layer)
                {
                    var frames = new List<WzDumper.WzData.Frame>();
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

                        var frameData = new WzDumper.WzData.Frame() {
                            durationMs = (frame.Delay * 1000) / 60, // convert to milliseconds, devide by 60 fps
                            position = new WzDumper.WzData.Point() {
                                x = x,
                                y = y
                            }
                        };

                        if (frame.Source is WzCanvasProperty canvas)
                        {
                            frameData.bitmapPath = canvas.FullPath.Replace('.', '_');

                            if (!assets.bitmaps.ContainsKey(frameData.bitmapPath))
                            {
                                assets.bitmaps[frameData.bitmapPath] = canvas.GetLinkedWzCanvasBitmap();
                            }
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }

                        frames.Add(frameData);
                    }

                    var tileData = new WzDumper.WzData.Tile() {
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
