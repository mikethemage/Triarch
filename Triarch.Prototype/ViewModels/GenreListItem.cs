using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels;

public class GenreListItem : ViewModelBase
{
    private bool _isSelected = false;

    public string DisplayText { get; set; } = string.Empty;
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
    public Genre Model { get; set; } = null!;
}