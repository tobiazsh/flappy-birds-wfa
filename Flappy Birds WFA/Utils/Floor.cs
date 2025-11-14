using Flappy_Birds_WFA.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Birds_WFA.Utils
{
    public class Floor
    {
        public static readonly Picture TEXTURE;

        static Floor()
        {
            TEXTURE = (Picture)ResourceHandler.GetResource(Identifier.Of(Globals.NamespaceName, "ground"));
        }

        public float X, Y, Width, Height;

        public Floor SetBounds(float width, float height)
        {
            this.Width = width;
            this.Height = height;
            return this;
        }

        public Floor SetPosition(float x, float y)
        {
            this.X = x;
            this.Y = y;
            return this;
        }

        public void Draw(PaintEventArgs e)
        {
            if (TEXTURE == null)
                throw new NullReferenceException("TEXTURE for Floor is null!");
            Graphics paintGraphics = e.Graphics;
            paintGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            paintGraphics.DrawImage(TEXTURE.Bitmap!, X, Y, Width, Height);
        }
    }
}
