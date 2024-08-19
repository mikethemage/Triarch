using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels.SystemEditor;

public class ElementTypeListItemViewModel : ViewModelBase
{
    private bool _isSelected;

    public string DisplayText { get { return Model.TypeName; } }
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
    public required RPGElementType Model {  get; set; }

    public void RefreshDisplayText()
    {
        OnPropertyChanged(nameof(DisplayText));
    }
}