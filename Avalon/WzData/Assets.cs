using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzDumper.WzData.Assets
{
    public struct BitmapFrame
    {
        public string bitmapPath;
        public WzData.Point offset;
    }

    public struct BitmapSprite
    {
        public List<BitmapFrame> frames;
        public int fps;
    }

    public enum SpriteType
    {
        Unknown,
        Bitmap
    }

    public struct Sprite
    {
        public string path;
        public SpriteType spriteType;
        public object spriteData;
    }
}
