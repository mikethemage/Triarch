using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Services;
using Triarch.Dtos.Definitions;
using Triarch.Prototype.ViewModels.MainMenu;

namespace Triarch.Prototype.ViewModels.SystemEditor;

public class ElementTypeEditorViewModel : ViewModelBase, IPageViewModel
{
    private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    public ElementTypeEditorViewModel(RPGSystem rPGSystem, string filePath = "")
    {
        _rPGSystem = rPGSystem;    
        _filePath= filePath;        
        BackCommand = new RelayCommand(Back, CanBack);
        _elementTypes = new ObservableCollection<ElementTypeListItemViewModel> ( rPGSystem.ElementTypes.Select(x => new ElementTypeListItemViewModel { IsSelected = false, Model = x }).ToList() );
    }

    public ElementTypeListItemViewModel? SelectedElementTypeItem
    {
        get
        {
            return _selectedElementTypeItem;
        }
        set
        {
            _selectedElementTypeItem = value;
            OnPropertyChanged(nameof(SelectedElementTypeItem));
        }
    }

    public required MainWindowViewModel Parent { get; set; }

    public bool ChangesSaved { get; set; } = false;
    
    public RelayCommand? BackCommand { get; set; }

    public ObservableCollection<ElementTypeListItemViewModel> ElementTypes
    {
        get
        {
            return _elementTypes;
        }
        set
        {
            _elementTypes = value;
            OnPropertyChanged(nameof(ElementTypes));
        }
    }

    public string FileName
    {
        get
        {
            if (_filePath == "")
            {
                return "";
            }
            else
            {
                return Path.GetFileName(_filePath);
            }
        }
    }

    private readonly RPGSystem _rPGSystem;

    private string _filePath;
    private ObservableCollection<ElementTypeListItemViewModel> _elementTypes;
    private ElementTypeListItemViewModel? _selectedElementTypeItem;

    public void Back()
    {        
        Parent.CurrentPage = new SystemEditorViewModel(_rPGSystem, _filePath) { Parent = Parent, ChangesSaved = ChangesSaved };
    }

    public bool CanBack()
    {
        return true;
    }    
}
