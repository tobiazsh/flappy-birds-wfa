using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Birds_WFA.Exceptions
{
    public class ResourceNotFoundException(string message, Exception? e) : FileNotFoundException(message, e)
    {

    }
}
