using System.IO;
using System.Text.Json;
using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Services;
using Triarch.Dtos.Definitions;
using Triarch.Prototype.Models;

namespace Triarch.Prototype.Services;
internal class RPGSystemFileProvider : IRPGSystemProvider
{
    List<SystemListItem>? _systemList;
    public void AddSystem(string name, RPGSystem system)
    {
        throw new NotImplementedException();
    }

    public RPGSystem LoadSystem(string name)
    {
        if (_systemList is null)
        {
            RefreshSystemList();
        }

        string? systemFileName = _systemList?.Where(x => x.SystemName == name).Select(x => x.FileName).FirstOrDefault();
        if (systemFileName == null)
        {
            throw new Exception($"Unable to find System: {name}");
        }

        string systemText = File.ReadAllText(systemFileName);
        RPGSystemDto? outputSystemDto = JsonSerializer.Deserialize<RPGSystemDto>(systemText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (outputSystemDto == null)
        {
            throw new Exception($"Unable to load file: {systemFileName}");
        }

        RPGSystemMapper mapper = new RPGSystemMapper();
        RPGSystem loadedSystem = mapper.Deserialize(outputSystemDto);

        if (loadedSystem == null)
        {
            throw new Exception($"Unable to load file: {systemFileName}");
        }

        return loadedSystem;
    }

    public List<SystemListItem> ListSystems()
    {
        if (_systemList is null)
        {
            RefreshSystemList();
        }

        return _systemList!;
    }

    private void RefreshSystemList()
    {
        string systemListText = File.ReadAllText("Datafiles" + Path.DirectorySeparatorChar + "SystemList.json");
        _systemList = JsonSerializer.Deserialize<List<SystemListItem>>(systemListText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (_systemList is null)
        {
            throw new Exception("Failed to load system list");
        }
    }
}
