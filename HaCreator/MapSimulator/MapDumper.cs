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
        public struct MapData
        {
            public struct Layer
            {
                public struct Tile
                {
                    public struct Frame
                    {
                        public string bitmapPath;
                        public int durationMs;
                        public Point position;
                    }

                    public List<Frame> frames;
                }

                public List<Tile> tiles;
            }

            public struct Assets
            {
                public Dictionary<string, Bitmap> bitmaps; // BitmapPath -> Bitmap
            }

            public Size mapSize;
            public Assets assets;

            public List<Layer> layers;
        }


        public MapData DumpMap()
        {
            var mapData = new MapData() {
                mapSize = new Size(vr_fieldBoundary.Width - vr_fieldBoundary.X, vr_fieldBoundary.Height - vr_fieldBoundary.Y),
                assets = new MapData.Assets() {
                    bitmaps = new Dictionary<string, Bitmap>()
                }
            };

            Console.WriteLine($"Map size: {{{mapData.mapSize.Width} {mapData.mapSize.Height}}}");
            Console.WriteLine($"Dumping tiles...");
            DumpTiles(ref mapData);

            return mapData;
        }

        private void DumpTiles(ref MapData mapData)
        {
            var layers = new List<MapData.Layer>();
            foreach (var (layer, layer_index) in mapObjects.Select((value, index) => (value, index)))
            {
                Console.Write($"  Dumping layer {layer_index}... ");

                var layerData = new MapData.Layer() {
                    tiles = new List<MapData.Layer.Tile>()
                };

                foreach (var obj in layer)
                {
                    var frames = new List<MapData.Layer.Tile.Frame>();
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

                        var frameData = new MapData.Layer.Tile.Frame();
                        frameData.durationMs = (frame.Delay * 1000) / 60; // convert to milliseconds, devide by 60 fps
                        frameData.position = new Point(x, y);

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

                    var tileData = new MapData.Layer.Tile() {
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
