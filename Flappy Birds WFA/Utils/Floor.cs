using Flappy_Birds_WFA.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Birds_WFA.Utils
{
    public class Floor : GameObject<Floor>
    {
        public static readonly Picture TEXTURE;

        static Floor()
        {
            TEXTURE = (Picture)ResourceHandler.GetResource(Identifier.Of(Globals.NamespaceName, "ground"));
        }

        // Implmented from GameObject
        public override void Draw(PaintEventArgs e)
        {
            if (TEXTURE == null)
                throw new NullReferenceException("TEXTURE for Floor is null!");

            Graphics paintGraphics = e.Graphics;
            paintGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            paintGraphics.DrawImage(TEXTURE.Bitmap!, X, Y, Width, Height);
        }

        public void Scroll(float amount)
        {
            this.X -= amount;
        }
    }
}
