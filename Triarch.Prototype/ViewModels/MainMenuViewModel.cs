using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;
using Triarch.BusinessLogic.Services;
using Triarch.Dtos.Definitions;
using Triarch.Prototype.Views;

namespace Triarch.Prototype.ViewModels;

public class MainMenuViewModel : IPageViewModel
{
    public MainMenuViewModel()
    {
        EditNewEntityCommand = new RelayCommand(EditNewEntity, CanEditNewEntity);
    }

    public RelayCommand EditNewEntityCommand { get; set; }

    public ObservableCollection<string> SystemSelector { get; set; } = new ObservableCollection<string> { "BESM3E" };
    public string? SelectedSystem { get; set; }

    public required MainWindowViewModel Parent { get; set; }

    public bool CanEditNewEntity()
    {
        return true;
    }

    public void EditNewEntity()
    {
        if (SelectedSystem == "BESM3E")
        {
            //MessageBox.Show("Test");

            string fileData = File.ReadAllText("DataFiles" + Path.DirectorySeparatorChar + SelectedSystem + ".json");
            RPGSystemDto? systemData = JsonSerializer.Deserialize<RPGSystemDto>(fileData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (systemData != null)
            {
                RPGSystemMapper mapper = new RPGSystemMapper();
                RPGSystem loadedSystem = mapper.Deserialize(systemData);

                if (loadedSystem != null)
                {

                    RPGSystemProvider rPGSystemProvider = new RPGSystemProvider();
                    rPGSystemProvider.AddSystem("BESM 3rd Edition", loadedSystem);

                    //string characterData = File.ReadAllText("D:\\Stuff\\RPGs\\BESM\\BESM 3rd Edition\\BESM Characters\\Converted\\test9.json");
                    //EntityDto? characterDto = JsonSerializer.Deserialize<EntityDto>(characterData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    //if (characterDto != null)
                    //{

                    //    RPGEntityMapper entityMapper = new RPGEntityMapper(rPGSystemProvider);
                    RPGEntity entity = new RPGEntity
                    {
                        RPGSystem = loadedSystem,
                        EntityName = "New Character",
                        EntityType = "Character",
                        Genre = loadedSystem.Genres[0]

                    };//entityMapper.Deserialize(characterDto);
                    entity.RootElement = loadedSystem.ElementDefinitions.Where(x => x.ElementName == "Character").First().CreateNode(entity, "", false);





                    //EntityDto outputDto = entityMapper.Serialize(entity);
                    //string outputText = JsonSerializer.Serialize(outputDto, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, DefaultIgnoreCondition=System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull });

                    //File.WriteAllText("D:\\Stuff\\RPGs\\BESM\\BESM 3rd Edition\\BESM Characters\\Converted\\test10.json", outputText);

                    EntityViewModel entityViewModel = new EntityViewModel(entity) { Parent = Parent};

                    Parent.CurrentPage = entityViewModel;

                    //}
                }

            }


        }
    }
}