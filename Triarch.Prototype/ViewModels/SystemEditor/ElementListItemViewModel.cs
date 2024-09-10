using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels.SystemEditor;

internal class ElementListItemViewModel : ViewModelBase
{
    private bool _isSelected;

    public string DisplayText { get { return Model.ElementName; } }
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
    public required RPGElementDefinition Model { get; set; }

    public void RefreshDisplayText()
    {
        OnPropertyChanged(nameof(DisplayText));
    }
}