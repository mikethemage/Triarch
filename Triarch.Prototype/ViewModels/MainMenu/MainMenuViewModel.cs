using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;
using Triarch.BusinessLogic.Services;
using Triarch.Dtos.Entities;
using Triarch.Prototype.Models;
using Triarch.Prototype.Services;
using Triarch.Prototype.ViewModels.EntityEditor;

namespace Triarch.Prototype.ViewModels.MainMenu;

public class MainMenuViewModel : ViewModelBase, IPageViewModel
{
    private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    private SystemListItem? _selectedSystem;

    private readonly RPGSystemFileProvider _rPGSystemProvider = new RPGSystemFileProvider();

    public MainMenuViewModel()
    {
        EditExistingEntityCommand = new RelayCommand(EditExistingEntity, CanEditExistingEntity);
        EditNewEntityCommand = new RelayCommand(EditNewEntity, CanEditNewEntity);
        EditExistingSystemCommand = new RelayCommand(EditExistingSystem, CanEditExistingSystem);
        ExitCommand = new RelayCommand(Exit, CanExit);
        ImportOldFormatEntityCommand = new RelayCommand(ImportOldFormatEntity, CanImportOldFormatEntity);
        EditNewSystemCommand = new RelayCommand(EditNewSystem, CanEditNewSystem);
        SystemSelector = new ObservableCollection<SystemListItem>(_rPGSystemProvider.ListSystems());
        if (SystemSelector.Count > 0)
        {
            SelectedSystem = SystemSelector[0];
        }
    }

    private bool CanEditNewSystem()
    {
        return false;
    }

    private void EditNewSystem()
    {
        throw new NotImplementedException();
    }

    private bool CanImportOldFormatEntity()
    {
        return true;
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
        return false; // SelectedSystem != null;
    }

    private bool CanEditExistingEntity()
    {
        return true;
    }

    public RelayCommand EditExistingEntityCommand { get; set; }
    public RelayCommand EditNewEntityCommand { get; set; }
    public RelayCommand EditExistingSystemCommand { get; set; }
    public RelayCommand ExitCommand { get; set; }
    public RelayCommand ImportOldFormatEntityCommand { get; set; }
    public RelayCommand EditNewSystemCommand { get; set; }

    public ObservableCollection<SystemListItem> SystemSelector { get; set; }

    public SystemListItem? SelectedSystem
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
        return SelectedSystem is not null;
    }

    public void EditNewEntity()
    {
        if (SelectedSystem is not null)
        {
            RPGSystem loadedSystem = _rPGSystemProvider.LoadSystem(SelectedSystem.SystemName);

            RPGEntity entity = new RPGEntity
            {
                RPGSystem = loadedSystem,
                EntityName = "New Character",
                EntityType = "Character",
                Genre = loadedSystem.Genres[0]

            };
            entity.RootElement = loadedSystem.ElementDefinitions.Where(x => x.ElementName == "Character").First().CreateNode(entity, "", false);

            Parent.CurrentPage = new EntityEditorViewModel(entity) { Parent = Parent };
        }
    }

    private void EditExistingEntity()
    {
        OpenFileDialog openDialog = new OpenFileDialog
        {
            RestoreDirectory = false,
            Filter = "Triarch" + " Files(*.json)|*.json|All Files (*.*)|*.*",
            FilterIndex = 1
        };

        if (openDialog.ShowDialog() ?? false)
        {
            try
            {
                string fileText = File.ReadAllText(openDialog.FileName);
                EntityDto? entityDto = JsonSerializer.Deserialize<EntityDto>(fileText, _serializerOptions);
                if (entityDto != null)
                {
                    string systemName = entityDto.RPGSystemName;
                    RPGSystem loadedSystem = _rPGSystemProvider.LoadSystem(systemName);
                    RPGEntityMapper rPGEntityMapper = new RPGEntityMapper(_rPGSystemProvider);
                    RPGEntity entity = rPGEntityMapper.Deserialize(entityDto);

                    Parent.CurrentPage = new EntityEditorViewModel(entity, openDialog.FileName) { Parent = Parent, ChangesSaved = true };
                }
            }
            catch
            {
                MessageBox.Show($"Error loading file {openDialog.FileName}", "File Open Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void ImportOldFormatEntity()
    {
        const string oldSystemName = "BESM 3rd Edition";
        if (!_rPGSystemProvider.ListSystems().Any(x => x.SystemName == oldSystemName))
        {
            throw new Exception("No available systems to import old format files!");
        }

        RPGSystem loadedSystem = _rPGSystemProvider.LoadSystem(oldSystemName);

        OpenFileDialog openDialog = new OpenFileDialog
        {
            RestoreDirectory = false,
            Filter = "BESM3CA" + " Files(*.xml)|*.xml|All Files (*.*)|*.*",
            FilterIndex = 1
        };

        if (openDialog.ShowDialog() ?? false)
        {
            try
            {
                string fileText = File.ReadAllText(openDialog.FileName);
                RPGEntityOldFormatLoader rPGEntityOldFormatLoader = new RPGEntityOldFormatLoader();
                RPGEntity? entity = rPGEntityOldFormatLoader.Load(fileText, loadedSystem);
                if (entity != null)
                {
                    Parent.CurrentPage = new EntityEditorViewModel(entity, "") { Parent = Parent, ChangesSaved = true };
                }
            }
            catch
            {
                MessageBox.Show($"Error loading file {openDialog.FileName}", "File Open Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}