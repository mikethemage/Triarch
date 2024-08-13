using System.Collections.ObjectModel;
using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels;

public class VariantListViewModel : ViewModelBase
{
    private readonly EntityElementViewModel _owner;
    private ObservableCollection<VariantListItemViewModel> _variantList = new ObservableCollection<VariantListItemViewModel>();
    private VariantListItemViewModel _selected = null!;

    public VariantListViewModel(Levelable model, EntityElementViewModel owner)
    {
        _owner = owner;
        if (model.AssociatedDefinition is LevelableDefinition levelableDefinition &&
            levelableDefinition.Variants != null &&
            levelableDefinition.Variants.Count > 0)
        {
            VariantList = new ObservableCollection<VariantListItemViewModel>(levelableDefinition.Variants.Select(x => new VariantListItemViewModel(x)).ToList());
            if (model.Variant != null)
            {
                var selected = VariantList.Where(x => x.VariantDefinitionData == model.Variant).FirstOrDefault();
                if (selected != null)
                {
                    selected.IsSelected = true;
                    _selected = selected;
                }
            }
        }
        else
        {
            throw new Exception("Variant list error!");
        }
    }

    public ObservableCollection<VariantListItemViewModel> VariantList
    {
        get
        {
            return _variantList;
        }
        private set
        {
            _variantList = value;
            OnPropertyChanged(nameof(VariantList));
        }
    }

    public VariantListItemViewModel Selected
    {
        get
        {
            return _selected;
        }
        set
        {
            _selected = value;
            _owner.SetVariant(_selected.VariantDefinitionData);
            OnPropertyChanged(nameof(Selected));
        }
    }
}