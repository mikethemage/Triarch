using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels.SystemEditor;

internal class ProgressionListItemViewModel : ViewModelBase
{
    private bool _isSelected;

    public string DisplayText { get { return Model.ProgressionType; } }
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
    public required Progression Model { get; set; }

    public void RefreshDisplayText()
    {
        OnPropertyChanged(nameof(DisplayText));
    }
}