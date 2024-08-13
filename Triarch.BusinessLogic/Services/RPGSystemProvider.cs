using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.BusinessLogic.Services;
public class RPGSystemProvider : IRPGSystemProvider
{
    private readonly Dictionary<string, RPGSystem> _systemList = new();

    public RPGSystem LoadSystem(string name)
    {
        return _systemList[name];
    }

    public void AddSystem(string name, RPGSystem system)
    {
        _systemList.Add(name, system);
    }
}
