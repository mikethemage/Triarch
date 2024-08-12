using System.Collections.ObjectModel;
using System.ComponentModel;
using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels;

public class EntityElementListItemViewModel : ViewModelBase
{
    private string _displayText = "";
    public string DisplayText
    {
        get { return _displayText; }
        set
        {
            _displayText = value;
            OnPropertyChanged(nameof(DisplayText));
        }
    }    

    public RPGElement ElementData { get; private set; }

    public EntityElementListItemViewModel(RPGElement element, EntityElementsListViewModel owner)
    {
        ElementData = element;
        _owner = owner;
        DisplayText = element.Name;
        foreach (RPGElement child in element.Children)
        {
            EntityElementListItemViewModel newElement = new EntityElementListItemViewModel(child, owner);
            Children.Add(newElement);
            owner.ElementList.Add(child, newElement);
        }
    }

    private bool _isSelected = false;
    private readonly EntityElementsListViewModel _owner;

    public bool IsSelected
    {
        get
        { return _isSelected; }
        set
        {
            _isSelected = value;
            OnPropertyChanged(nameof(IsSelected));
            if (value == true)
            {
                _owner.Selected = this;
            }
        }
    }

    public ObservableCollection<EntityElementListItemViewModel> Children { get; set; } = new ObservableCollection<EntityElementListItemViewModel>();
}
    