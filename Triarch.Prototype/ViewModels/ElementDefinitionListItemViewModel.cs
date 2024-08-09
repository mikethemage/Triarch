using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels;

public class ElementDefinitionListItemViewModel : ViewModelBase
{
    private bool _isSelected = false;

    public string DisplayName { get; set; } = null!;
    public RPGElementDefinition Model { get; set; } = null!;
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
}