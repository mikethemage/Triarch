using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels.SystemEditor;

internal class GenreListItemViewModel : ViewModelBase
{
    private bool _isSelected;

    public string DisplayText { get { return Model.GenreName; } }
    public bool IsSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            _isSelected = value;
            OnPropertyChanged(nameof(IsSelected));
        }
    }
    public required Genre Model { get; set; }

    public void RefreshDisplayText()
    {
        OnPropertyChanged(nameof(DisplayText));
    }
}