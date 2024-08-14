namespace Triarch.Repositories.Exceptions;

public class CoreRulesetNotFoundException : Exception
{

    public CoreRulesetNotFoundException(string message, string name) : base(message)
    {
        Name = name;
    }

    public string Name { get; private set; }
}
