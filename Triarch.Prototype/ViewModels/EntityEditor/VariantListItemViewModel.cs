using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels.EntityEditor;

public class VariantListItemViewModel : ViewModelBase
{
    private bool _isSelected = false;

    public VariantListItemViewModel(VariantDefinition variantDefinition)
    {
        VariantDefinitionData = variantDefinition;
        DisplayText = variantDefinition.VariantName;
    }
    public VariantDefinition VariantDefinitionData { get; private set; }
    public string DisplayText { get; private set; }
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