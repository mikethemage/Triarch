using System.Collections.ObjectModel;
using System.IO;
using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels.SystemEditor;

internal class ElementEditorViewModel : ViewModelBase, IPageViewModel
{
    public required MainWindowViewModel Parent { get; set; }

    public bool ChangesSaved { get; set; } = false;

    public ElementEditorViewModel(RPGSystem rPGSystem, string filePath = "")
    {
        _rPGSystem = rPGSystem;
        _filePath = filePath;
        BackCommand = new RelayCommand(Back, CanBack);
        _elements = new ObservableCollection<ElementListItemViewModel>(rPGSystem.ElementDefinitions.Select(x => new ElementListItemViewModel { IsSelected = false, Model = x }).ToList());
    }

    public ObservableCollection<ElementListItemViewModel> Elements
    {
        get
        {
            return _elements;
        }
        set
        {
            _elements = value;
            OnPropertyChanged(nameof(Elements));
        }
    }

    private ObservableCollection<ElementListItemViewModel> _elements;
    private ElementListItemViewModel? _selectedElementItem;
    public RelayCommand? BackCommand { get; set; }
    private readonly RPGSystem _rPGSystem;
    private string _filePath;
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

    public void Back()
    {
        Parent.CurrentPage = new SystemEditorViewModel(_rPGSystem, _filePath) { Parent = Parent, ChangesSaved = ChangesSaved };
    }

    public bool CanBack()
    {
        return true;
    }
}