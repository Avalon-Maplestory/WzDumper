using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HaCreator.MapSimulator.MapSimulator;

namespace WzDumper.Map
{
    public static class MapDataPreview
    {
        public static void ShowMapPreview(this WzData.Map.MapData mapData, Dictionary<string, System.Drawing.Bitmap> bitmaps, bool procedural = false)
        {
            var bitmap = new Bitmap(mapData.mapSize.width, mapData.mapSize.height);
            var graphics = Graphics.FromImage(bitmap);

            using (var form = new Form())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Size = new Size(1600, 900);

                var pictureBox = new PictureBox();
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.Image = bitmap;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                form.Controls.Add(pictureBox);

                // Background[Back]
                form.Shown += (object sender, EventArgs e) => { RenderTiles(mapData, bitmaps, graphics, pictureBox, procedural); };
                // Portals
                // Reactors
                // Life (NPC + Mobs)
                // Life[Mob]
                // Life[NPC]
                // Background[Front]
                // Tooltips
                // Minimap
                
                form.ShowDialog();
            }
        }

        private static void RenderTiles(WzData.Map.MapData mapData, Dictionary<string, System.Drawing.Bitmap> bitmaps, Graphics graphics, PictureBox pictureBox, bool procedural)
        {
            foreach (var layer in mapData.layers)
            {
                foreach (var tile in layer.tiles)
                {
                    RenderSprite(tile.sprite, tile.position, bitmaps, graphics);
                    
                    if (procedural)
                    {
                        pictureBox.Refresh();
                    }
                }
            }
        }

        private static void RenderSprite(WzData.Assets.Sprite sprite, WzData.Point position, Dictionary<string, System.Drawing.Bitmap> bitmaps, Graphics graphics)
        {
            if (sprite.spriteType == WzData.Assets.SpriteType.Bitmap)
            {
                var spriteData = (WzData.Assets.BitmapSprite)sprite.spriteData;

                var frame = spriteData.frames[0];
                graphics.DrawImage(bitmaps[frame.bitmapPath], position.x + frame.offset.x, position.y + frame.offset.y);
            }
        }
    }
}
