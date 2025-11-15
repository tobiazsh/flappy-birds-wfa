using Flappy_Birds_WFA.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Birds_WFA.Utils
{
    public class Pipe : GameObject<Pipe>
    {
        public static readonly Picture TEXTURE;

        static Pipe()
        {
            TEXTURE = (Picture)ResourceHandler.GetResource(Identifier.Of(Globals.NamespaceName, "pipe"));
        }

        public enum PipeType
        {
            TOP,
            BOTTOM
        }

        public Pipe(PipeType type)
        {
            this.Type = type;
        }

        public PipeType Type { get; private set; }

        public override void Draw(PaintEventArgs e)
        {
            if (TEXTURE == null)
                throw new NullReferenceException("TEXTURE for Pipe is null!");

            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int rotation = Type == PipeType.TOP ? 0 : 180;

            // Only rotate if necessary
            if (rotation == 0 || rotation % 360 == 0)
            {
                graphics.TranslateTransform((float)(X + Width / 2), (float)(Y + Height / 2)); // Move to center of Pipe
                graphics.RotateTransform(rotation); // Rotate
                graphics.TranslateTransform(-(float)(X + Width / 2), -(float)(Y + Height / 2)); // Move back
            }

            graphics.DrawImage(TEXTURE.Bitmap!, X, Y, Width, Height);
        }

        public void Scroll(float amount)
        {
            this.X -= amount;
        }
    }

    public class PipePair
    {
        public Pipe TopPipe { get; init; }
        public Pipe BottomPipe { get; init; }

        public PipePair(float topPipeHeight, float bottomPipeHeight, float availableHeight, float xPosition, float pipeWidth)
        {
            TopPipe = new Pipe(Pipe.PipeType.TOP);
            BottomPipe = new Pipe(Pipe.PipeType.BOTTOM);

            TopPipe
                .SetPosition(xPosition, 0)
                .SetBounds(pipeWidth, topPipeHeight);

            BottomPipe
                .SetPosition(xPosition, availableHeight - bottomPipeHeight)
                .SetBounds(pipeWidth, bottomPipeHeight);
        }

        public static PipePair GenerateRandom(float minGapHeight, float maxGapHeight, float minPipeWidth, float maxPipeWidth, float availableHeight, float x)
        {
            Random random = new Random();
            float gapHeight = (float) random.NextDouble() * (maxGapHeight - minGapHeight) + minGapHeight; // Generate Random Gap Height
            float pipeWidth = (float) random.NextDouble() * (maxPipeWidth - minPipeWidth) + minPipeWidth; // Generate Random Pipe Width
            float topPipeHeight = (float) random.NextDouble() * (availableHeight - gapHeight); // Random Top Pipe Height

            return new PipePair(
                topPipeHeight,
                availableHeight - gapHeight - topPipeHeight,
                availableHeight,
                x,
                pipeWidth
            );
        }

        public void Scroll(float amount)
        {
            TopPipe.Scroll(amount);
            BottomPipe.Scroll(amount);
        }
    }
}
