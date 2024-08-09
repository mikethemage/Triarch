using System.Collections.ObjectModel;
using System.ComponentModel;
using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels;

public class VariantListViewModel : INotifyPropertyChanged
{
    private ObservableCollection<VariantListItemViewModel> _variantList = new ObservableCollection<VariantListItemViewModel>();

    public VariantListViewModel(Levelable model)
    {
        if(model.AssociatedDefinition is LevelableDefinition levelableDefinition &&
            levelableDefinition.Variants != null &&
            levelableDefinition.Variants.Count > 0)
        {
            VariantList = new ObservableCollection<VariantListItemViewModel>(levelableDefinition.Variants.Select(x=>new VariantListItemViewModel(x)).ToList());
            if(model.Variant!=null)
            {
                var selected = VariantList.Where(x => x.VariantDefinitionData == model.Variant).FirstOrDefault();
                if (selected != null)
                {
                    selected.IsSelected = true;
                    Selected = selected;
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
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public VariantListItemViewModel Selected {  get; set; } = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
}