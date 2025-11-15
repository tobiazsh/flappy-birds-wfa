using Flappy_Birds_WFA.Resource;
using Flappy_Birds_WFA.Utils;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Birds_WFA.GameObjects
{
    public class Bird : GameObject<Bird>
    {
        public enum BirdRotation
        {
            UP = -25,
            NORMAL = 0,
            DOWN = 25
        }

        public static readonly Picture TEXTURE;

        static Bird()
        {
            TEXTURE = (Picture)ResourceHandler.GetResource(Identifier.Of(Globals.NamespaceName, "bird")); // Get Bird Texture from Resources
        }

        public float JumpCounter { get; set; }
        public BirdRotation Rotation { get; set; } = BirdRotation.NORMAL;

        public const float GRAVITY = 1.5f;
        public const float JUMP = 75f;
        public const float JUMP_SPEED = 3f;

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
                Rotation = BirdRotation.UP;

                float jumpAmount = Math.Min(JUMP_SPEED, JumpCounter);
                JumpCounter -= jumpAmount; // Decrease Jump Counter
                
                if (Y - jumpAmount > maxHeight)
                    Y -= jumpAmount; // Move Up
                else
                {
                    Rotation = BirdRotation.NORMAL;
                    Y = maxHeight; // Cap at minHeight | Maybe here rotate into normal position
                }
            }
            else // Apply Gravity if Bird should not jump
            {
                // Maybe set rotation to downwards for aesthetics here?
                Rotation = BirdRotation.DOWN;

                if (Y + GRAVITY < minHeight)
                    Y += GRAVITY; // Move Down
                else 
                {
                    Rotation = BirdRotation.NORMAL;
                    Y = minHeight; // Cap at maxHeight | Maybe here rotate into normal position
                }
            }
        }

        // Implemented from GameObject
        public override void Draw(PaintEventArgs e)
        {
            if (TEXTURE == null)
                throw new NullReferenceException("TEXTURE for Bird is null!");
            Graphics paintGraphics = e.Graphics;

            var state = paintGraphics.Save();

            paintGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Rotate Bird
            paintGraphics.TranslateTransform((float)(X + Width / 2), (float)(Y + Height / 2)); // Move to center of Bird
            paintGraphics.RotateTransform((float)Rotation); // Rotate Bird
            paintGraphics.TranslateTransform(-(float)(X + Width / 2), -(float)(Y + Height / 2)); // Move back

            // Draw Bird
            paintGraphics.DrawImage(TEXTURE.Bitmap!, X, Y, Width, Height); // Draw Bird :=)

            paintGraphics.Restore(state);
        }
    }
}
