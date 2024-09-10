using System.Collections.ObjectModel;
using System.IO;
using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels.SystemEditor;

internal class GenreEditorViewModel : ViewModelBase, IPageViewModel
{
    private readonly RPGSystem _rPGSystem;
    private string _filePath;
    private ObservableCollection<GenreListItemViewModel> _genres;

    public required MainWindowViewModel Parent { get; set; }
    public bool ChangesSaved { get; set; }

    public GenreEditorViewModel(RPGSystem rPGSystem, string filePath = "")
    {
        _rPGSystem = rPGSystem;
        _filePath = filePath;
        BackCommand = new RelayCommand(Back, CanBack);
        _genres = new ObservableCollection<GenreListItemViewModel>(rPGSystem.Genres.Select(x => new GenreListItemViewModel { IsSelected = false, Model = x }).ToList());
    }
    public RelayCommand? BackCommand { get; set; }

    private GenreListItemViewModel? _selectedGenreItem;

    public void Back()
    {
        Parent.CurrentPage = new SystemEditorViewModel(_rPGSystem, _filePath) { Parent = Parent, ChangesSaved = ChangesSaved };
    }

    public bool CanBack()
    {
        return true;
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

    public ObservableCollection<GenreListItemViewModel> Genres
    {
        get
        {
            return _genres;
        }
        set
        {
            _genres = value;
            OnPropertyChanged(nameof(Genres));
        }
    }
}