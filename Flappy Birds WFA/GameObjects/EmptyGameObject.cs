using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Birds_WFA.GameObjects
{
    public class EmptyGameObject : GameObject<EmptyGameObject>
    {
        public override void Draw(PaintEventArgs e)
        {
            return; // Do nothing
        }
    }
}
