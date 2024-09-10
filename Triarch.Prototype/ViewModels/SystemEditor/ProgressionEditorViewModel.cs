using System.Collections.ObjectModel;
using System.IO;
using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels.SystemEditor;

internal class ProgressionEditorViewModel : ViewModelBase, IPageViewModel
{
    public required MainWindowViewModel Parent { get; set; }
    public bool ChangesSaved { get; set; }

    public ProgressionEditorViewModel(RPGSystem rPGSystem, string filePath = "")
    {
        _rPGSystem = rPGSystem;
        _filePath = filePath;
        BackCommand = new RelayCommand(Back, CanBack);
        _progressions = new ObservableCollection<ProgressionListItemViewModel>(rPGSystem.Progressions.Select(x => new ProgressionListItemViewModel { IsSelected = false, Model = x }).ToList());

    }

    public ProgressionListItemViewModel? SelectedProgressionItem
    {
        get
        {
            return _selectedProgressionItem;
        }
        set
        {
            _selectedProgressionItem = value;
            OnPropertyChanged(nameof(SelectedProgressionItem));
        }
    }

    public RelayCommand? BackCommand { get; set; }

    public ObservableCollection<ProgressionListItemViewModel> Progressions
    {
        get
        {
            return _progressions;
        }
        set
        {
            _progressions = value;
            OnPropertyChanged(nameof(Progressions));
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
    private ObservableCollection<ProgressionListItemViewModel> _progressions;
    private ProgressionListItemViewModel? _selectedProgressionItem;

    public void Back()
    {
        Parent.CurrentPage = new SystemEditorViewModel(_rPGSystem, _filePath) { Parent = Parent, ChangesSaved = ChangesSaved };
    }

    public bool CanBack()
    {
        return true;
    }
}