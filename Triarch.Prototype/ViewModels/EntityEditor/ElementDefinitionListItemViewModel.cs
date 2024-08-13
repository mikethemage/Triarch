using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels.EntityEditor;

public class ElementDefinitionListItemViewModel : ViewModelBase
{
    public string DisplayName { get; set; } = null!;
    public string TypeName { get; set; } = null!;
    public RPGElementDefinition Model { get; set; } = null!;

    private bool _isSelected = false;

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