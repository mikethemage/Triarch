using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;
using Triarch.BusinessLogic.Services;
using Triarch.Dtos.Definitions;
using Triarch.Dtos.Entities;

namespace Triarch.Prototype.ViewModels;

public class MainMenuViewModel : ViewModelBase, IPageViewModel
{
    private string? _selectedSystem;

    public MainMenuViewModel()
    {
        EditExistingEntityCommand = new RelayCommand(EditExistingEntity, CanEditExistingEntity);
        EditNewEntityCommand = new RelayCommand(EditNewEntity, CanEditNewEntity);
        EditExistingSystemCommand = new RelayCommand(EditExistingSystem, CanEditExistingSystem);
        ExitCommand = new RelayCommand(Exit, CanExit);
    }

    private void EditExistingEntity()
    {
        OpenFileDialog openDialog = new OpenFileDialog
            {
                RestoreDirectory = false,
                Filter = "Triarch" + " Files(*.json)|*.json|All Files (*.*)|*.*",
                FilterIndex = 1
            };

        if(openDialog.ShowDialog() ?? false)
        {
            try
            {
                string fileText = File.ReadAllText(openDialog.FileName);
                EntityDto? entity = JsonSerializer.Deserialize<EntityDto>(fileText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (entity != null)
                {
                    MessageBox.Show("Loaded Entity to Dto");
                    
                }
            }
            catch 
            {
                MessageBox.Show($"Error loading file {openDialog.FileName}","File Open Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }
    }

    private void EditExistingSystem()
    {
        throw new NotImplementedException();
    }

    private bool CanExit()
    {
        return true;
    }

    private void Exit()
    {
        Parent.Exit();
    }

    private bool CanEditExistingSystem()
    {
        return SelectedSystem != null;
    }    

    private bool CanEditExistingEntity()
    {
        return true;
    }    

    public RelayCommand EditExistingEntityCommand { get; set; }
    public RelayCommand EditNewEntityCommand { get; set; }
    public RelayCommand EditExistingSystemCommand { get; set; }
    public RelayCommand ExitCommand { get; set; }

    public ObservableCollection<string> SystemSelector { get; set; } = new ObservableCollection<string> { "BESM3E" };
    public string? SelectedSystem
    {
        get
        {
            return _selectedSystem;
        }
        set
        {
            _selectedSystem = value;
            OnPropertyChanged(nameof(SelectedSystem));
            EditNewEntityCommand?.RaiseCanExecuteChanged();
            EditExistingSystemCommand?.RaiseCanExecuteChanged();
        }
    }

    public required MainWindowViewModel Parent { get; set; }

    public bool CanEditNewEntity()
    {
        return SelectedSystem is not null && SelectedSystem != "";
    }

    public void EditNewEntity()
    {
        if (SelectedSystem is not null && SelectedSystem != "")
        {   
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
                    
                    RPGEntity entity = new RPGEntity
                    {
                        RPGSystem = loadedSystem,
                        EntityName = "New Character",
                        EntityType = "Character",
                        Genre = loadedSystem.Genres[0]

                    };
                    entity.RootElement = loadedSystem.ElementDefinitions.Where(x => x.ElementName == "Character").First().CreateNode(entity, "", false);

                    Parent.CurrentPage = new EntityViewModel(entity) { Parent = Parent }; 
                }
            }
        }
    }
}