using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triarch.Repositories.Exceptions;

public class CoreRulesetNotFoundException : Exception
{

    public CoreRulesetNotFoundException(string message, int id) : base(message)
    {
        Id = id;
    }

    public int Id { get; private set; }
}
