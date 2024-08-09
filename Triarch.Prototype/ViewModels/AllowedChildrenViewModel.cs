﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels;

public class AllowedChildrenViewModel : INotifyPropertyChanged
{
    private ObservableCollection<FilterTypeViewModel> _filterList = new ObservableCollection<FilterTypeViewModel>();
    private FilterTypeViewModel _selectedFilter = null!;
    private ObservableCollection<ElementDefinitionListItemViewModel> _allowedChildrenList = new ObservableCollection<ElementDefinitionListItemViewModel>();
    private ElementDefinitionListItemViewModel? _selectedChild = null;

    private List<RPGElementDefinition> _allAllowedChildren = new List<RPGElementDefinition>();


    public AllowedChildrenViewModel(List<RPGElementDefinition> allowedChildren)
    {
        _allAllowedChildren = allowedChildren.Where(x => x.ElementType.BuiltIn == false).ToList();
        AllowedChildrenList = new ObservableCollection<ElementDefinitionListItemViewModel>(_allAllowedChildren.Select(x=>new ElementDefinitionListItemViewModel { Model=x, DisplayName=x.ElementName,IsSelected=false} ).ToList());

        FilterList = new ObservableCollection<FilterTypeViewModel>(_allAllowedChildren.Select(x=>x.ElementType).Distinct().OrderBy(x=>x.TypeOrder).Select(x=>new FilterTypeViewModel { DisplayName=x.TypeName, IsSelected=false, Model=x}).ToList());
        FilterList.Insert(0, new FilterTypeViewModel { DisplayName = "ALL", IsSelected = false, Model=null });
    }

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
    public FilterTypeViewModel SelectedFilter
    {
        get
        {
            return _selectedFilter;
        }
        set
        {
            _selectedFilter = value;
            AllowedChildrenList = new ObservableCollection<ElementDefinitionListItemViewModel>(_allAllowedChildren.Where(x=>x.ElementType==_selectedFilter.Model).Select(x => new ElementDefinitionListItemViewModel { Model = x, DisplayName = x.ElementName, IsSelected = false }).ToList());
            OnPropertyChanged(nameof(SelectedFilter));
        }
    }
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

    public bool CanAdd()
    {
        if (SelectedChild != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}