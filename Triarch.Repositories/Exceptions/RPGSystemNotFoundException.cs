using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triarch.Repositories.Exceptions;
public class RPGSystemNotFoundException : Exception
{
    public RPGSystemNotFoundException(string message) : base(message) 
    {
        
    }
}
