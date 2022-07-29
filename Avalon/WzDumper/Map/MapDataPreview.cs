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
        public static void ShowMapPreview(this WzData.MapData mapData, bool procedural = false)
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
                form.Shown += (object sender, EventArgs e) => { RenderTiles(mapData, graphics, pictureBox, procedural); };
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

        private static void RenderTiles(WzData.MapData mapData, Graphics graphics, PictureBox pictureBox, bool procedural)
        {
            foreach (var layer in mapData.layers)
            {
                foreach (var tile in layer.tiles)
                {
                    var frame = tile.frames[0];
                    graphics.DrawImage(mapData.assets.bitmaps[frame.bitmapPath], frame.position.x, frame.position.y);
                    if (procedural)
                    {
                        pictureBox.Refresh();
                    }
                }
            }
        }
    }
}
