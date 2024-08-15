using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels.EntityEditor;

public class AllowedChildrenViewModel : ViewModelBase
{
    public AllowedChildrenViewModel(List<RPGElementDefinition> allowedChildren)
    {
        _allAllowedChildren = allowedChildren.Where(x => x.ElementType.BuiltIn == false).ToList();
        AllowedChildrenList = new ObservableCollection<ElementDefinitionListItemViewModel>(_allAllowedChildren.Select(x => new ElementDefinitionListItemViewModel { Model = x, TypeName = x.ElementType.TypeName, DisplayName = x.ElementName, IsSelected = false }).OrderBy(x => x.Model.ElementType.TypeOrder).ThenBy(x => x.DisplayName).ToList());

        var collectionViewSource = new CollectionViewSource { Source = AllowedChildrenList };
        collectionViewSource.GroupDescriptions.Add(new PropertyGroupDescription("TypeName"));
        _groupedAllowedChildrenList = collectionViewSource.View;

        FilterList = new ObservableCollection<FilterTypeViewModel>(_allAllowedChildren.Select(x => x.ElementType).Distinct().OrderBy(x => x.TypeOrder).Select(x => new FilterTypeViewModel { DisplayName = x.TypeName, IsSelected = false, Model = x }).ToList());

        FilterTypeViewModel allFilter = new FilterTypeViewModel { DisplayName = "ALL", IsSelected = true, Model = null };
        FilterList.Insert(0, allFilter);
        _selectedFilter = allFilter;
    }

    private ElementDefinitionListItemViewModel? _selectedChild = null;

    private readonly List<RPGElementDefinition> _allAllowedChildren = new List<RPGElementDefinition>();

    private ICollectionView _groupedAllowedChildrenList;

    private ObservableCollection<FilterTypeViewModel> _filterList = new ObservableCollection<FilterTypeViewModel>();

    public ObservableCollection<FilterTypeViewModel> FilterList
    {
        get
        {
            return _filterList;
        }
        set
        {
            _filterList = value;
            OnPropertyChanged(nameof(FilterList));
        }
    }

    private FilterTypeViewModel _selectedFilter = null!;

    public FilterTypeViewModel SelectedFilter
    {
        get
        {
            return _selectedFilter;
        }
        set
        {
            _selectedFilter = value;
            if (_selectedFilter.DisplayName == "ALL")
            {
                AllowedChildrenList = new ObservableCollection<ElementDefinitionListItemViewModel>(_allAllowedChildren.Select(x => new ElementDefinitionListItemViewModel { Model = x, TypeName = x.ElementType.TypeName, DisplayName = x.ElementName, IsSelected = false }).OrderBy(x => x.Model.ElementType.TypeOrder).ThenBy(x => x.DisplayName).ToList());

            }
            else
            {
                AllowedChildrenList = new ObservableCollection<ElementDefinitionListItemViewModel>(_allAllowedChildren.Where(x => x.ElementType == _selectedFilter.Model).Select(x => new ElementDefinitionListItemViewModel { Model = x, TypeName = x.ElementType.TypeName, DisplayName = x.ElementName, IsSelected = false }).OrderBy(x => x.Model.ElementType.TypeOrder).ThenBy(x => x.DisplayName).ToList());
            }

            OnPropertyChanged(nameof(SelectedFilter));
        }
    }

    private ObservableCollection<ElementDefinitionListItemViewModel> _allowedChildrenList = new ObservableCollection<ElementDefinitionListItemViewModel>();

    public ObservableCollection<ElementDefinitionListItemViewModel> AllowedChildrenList
    {
        get
        {
            return _allowedChildrenList;
        }
        set
        {
            _allowedChildrenList = value;
            OnPropertyChanged(nameof(AllowedChildrenList));
            var collectionViewSource = new CollectionViewSource { Source = AllowedChildrenList };
            collectionViewSource.GroupDescriptions.Add(new PropertyGroupDescription("TypeName"));
            GroupedAllowedChildrenList = collectionViewSource.View;
        }
    }

    public ICollectionView GroupedAllowedChildrenList
    {
        get
        {
            return _groupedAllowedChildrenList;
        }
        set
        {
            _groupedAllowedChildrenList = value;
            OnPropertyChanged(nameof(GroupedAllowedChildrenList));
        }
    }

    public ElementDefinitionListItemViewModel? SelectedChild
    {
        get
        {
            return _selectedChild;
        }
        set
        {
            _selectedChild = value;
            OnPropertyChanged(nameof(SelectedChild));
        }
    }
}