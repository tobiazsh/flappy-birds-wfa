using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Birds_WFA.Utils
{
    public abstract class GameObject<T> where T : GameObject<T>
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public T SetPosition(float x, float y)
        {
            this.X = x;
            this.Y = y;
            return (T)this;
        }

        public T SetBounds(float width, float height)
        {
            this.Width = width;
            this.Height = height;
            return (T)this;
        }

        public bool IntersectsWith(GameObject<T> other)
        {
            return  this.X + this.Width      > other.X &&
                    this.Y + this.Height     > other.Y &&
                    this.X                   < other.X + other.Width &&
                    this.Y                   < other.Y + other.Height;
        }

        public abstract void Draw(PaintEventArgs e);
    }
}
