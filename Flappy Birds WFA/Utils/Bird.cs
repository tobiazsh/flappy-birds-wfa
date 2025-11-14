using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Birds_WFA.Utils
{
    public class Bird
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Velocity { get; set; }

        public const float GRAVITY = 0.6f;
        public const float JUMP = -10f; 

        public void Jump()
        {

        }
    }
}
