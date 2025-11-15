using Flappy_Birds_WFA.Resource;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Birds_WFA.Utils
{
    public class Bird
    {
        public static readonly Picture TEXTURE;

        static Bird()
        {
            TEXTURE = (Picture)ResourceHandler.GetResource(Identifier.Of(Globals.NamespaceName, "bird")); // Get Bird Texture from Resources
        }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float JumpCounter { get; set; }

        public const float GRAVITY = 0.6f;
        public const float JUMP = 100f;
        public const float JUMP_SPEED = 1f;

        public void Jump()
        {
            JumpCounter += JUMP;
        }

        /// <summary>
        /// Calculate Bird Position
        /// </summary>
        /// <param name="minHeight">The min height of the bird's position</param>
        /// <param name="maxHeight">The max height of the bird's position</param>
        public void Calculate(float minHeight, float maxHeight)
        {
            // Animate Falling/Jumping
            if (JumpCounter > 0) // Jump in Progress
            {
                // Maybe set rotation to upwards for aesthetics here?

                float jumpAmount = Math.Min(JUMP_SPEED, JumpCounter);
                JumpCounter -= jumpAmount; // Decrease Jump Counter
                
                if (Y - jumpAmount > maxHeight)
                    Y -= jumpAmount; // Move Up
                else
                    Y = maxHeight; // Cap at minHeight | Maybe here rotate into normal position
            }
            else // Apply Gravity if Bird should not jump
            {
                // Maybe set rotation to downwards for aesthetics here?

                if (Y + GRAVITY < minHeight)
                    Y += GRAVITY; // Move Down
                else
                    Y = minHeight; // Cap at maxHeight | Maybe here rotate into normal position
            }
        }

        public void Draw(PaintEventArgs e)
        {
            if (TEXTURE == null)
                throw new NullReferenceException("TEXTURE for Bird is null!");
            Graphics paintGraphics = e.Graphics;
            paintGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            paintGraphics.DrawImage(TEXTURE.Bitmap!, X, Y, Width, Height); // Draw Bird :=)
        }

        public Bird SetBounds(float width, float height)
        {
            this.Width = width;
            this.Height = height;
            return this;
        }

        public Bird SetPosition(float x, float y)
        {
            this.X = x;
            this.Y = y;
            return this;
        }
    }
}
