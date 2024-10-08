﻿using System.Collections.ObjectModel;
using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels.EntityEditor;

public class EntityElementsListViewModel : ViewModelBase
{
    public EntityElementsListViewModel(RPGEntity entity, EntityEditorViewModel owner)
    {
        _owner = owner;
        RootElements.Add(new EntityElementListItemViewModel(entity.RootElement, this));
        ElementList.Add(entity.RootElement, RootElements[0]);
        RootElements[0].IsSelected = true;
        _selected = RootElements[0];
    }

    public ObservableCollection<EntityElementListItemViewModel> RootElements { get; private set; } = new ObservableCollection<EntityElementListItemViewModel>();

    public Dictionary<RPGElement, EntityElementListItemViewModel> ElementList { get; set; } = new Dictionary<RPGElement, EntityElementListItemViewModel>();


    private EntityElementListItemViewModel _selected;
    private readonly EntityEditorViewModel _owner;

    public EntityElementListItemViewModel Selected
    {
        get { return _selected; }
        set
        {
            _selected = value;
            _owner.SelectedElement = new EntityElementViewModel(value.ElementData, _owner);
            OnPropertyChanged(nameof(Selected));
        }
    }

    public void RefreshElementDisplayText(RPGElement element)
    {
        ElementList[element].RefreshDisplayText();
    }

    internal void RefreshSelectedAndParentsDisplayText()
    {
        EntityElementListItemViewModel? currentElement = Selected;
        while (currentElement != null)
        {
            currentElement.RefreshDisplayText();
            if (currentElement.ElementData.Parent == null)
            {
                currentElement = null;
            }
            else
            {
                currentElement = ElementList[currentElement.ElementData.Parent];
            }
        }
    }
}