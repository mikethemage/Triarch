using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triarch.RPGSystem.Editor.WPF.Views;
using Triarch.RPGSystem.Models;

namespace Triarch.RPGSystem.Editor.WPF.ViewModels;
internal class EditElementsViewModel : INotifyPropertyChanged
{
    
    public bool EditItemShouldBeVisible
    {
        get
        {
            return CurrentlyEditingItem != null;
        }
    }

    public RPGElementDefinition? CurrentlyEditingItem
    {
        get
        {
            return currentlyEditingItem;
        }
        set
        {
            currentlyEditingItem = value;
            OnPropertyChanged(nameof(CurrentlyEditingItem));
            OnPropertyChanged(nameof(EditItemShouldBeVisible));
        }
    }

    private TriarchDbContext _context;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private Models.RPGSystem _rPGSystem;

    public EditElementsViewModel(TriarchDbContext context, Models.RPGSystem rPGSystem)
    {
        _context = context;
        _rPGSystem = rPGSystem;

        ElementsList = new(_context.Entry(_rPGSystem).Collection(x => x.ElementDefinitions).Query().OrderBy(x => x.ElementType.TypeOrder).ThenBy(x => x.ElementName).Select(x => new ElementSelectItem { Id = x.Id, Name = x.ElementName, ElementType = x.ElementType.TypeName }));
        
    }

    public void Edit()
    {
        if (SelectedItem != null)
        {
            var b = _context.RPGElementDefinitions.FirstOrDefault(x => x.Id == SelectedItem.Id);
            var a = new EditElementDefinitionViewModel(_context, b);
            a.ShowWindow();
        }
    }


    public void Create()
    {
        var a = new EditElementDefinitionViewModel(_context, _rPGSystem);
        a.ShowWindow();
    }

    public void Save()
    {
        if (CurrentlyEditingItem != null)
        {
            if (CurrentlyEditingItem.Id == 0)
            {
                _context.RPGElementDefinitions.Add(CurrentlyEditingItem);
            }
            _context.SaveChanges();

            ElementsList = new(_context.Entry(_rPGSystem).Collection(x => x.ElementDefinitions).Query().OrderBy(x => x.ElementType.TypeOrder).ThenBy(x => x.ElementName).Select(x => new ElementSelectItem { Id = x.Id, Name = x.ElementName, ElementType=x.ElementType.TypeName }));
            SelectedItem = ElementsList.FirstOrDefault(x => x.Id == CurrentlyEditingItem.Id);
            CurrentlyEditingItem = null;
        }
    }

    private ElementSelectItem? selectedItem;
    public ElementSelectItem? SelectedItem
    {
        get
        {
            return selectedItem;
        }
        set
        {
            selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));
        }
    }

    private ObservableCollection<ElementSelectItem> elementsList = null!;
    private RPGElementDefinition? currentlyEditingItem;

    public ObservableCollection<ElementSelectItem> ElementsList
    {
        get
        {
            return elementsList;
        }
        set
        {
            elementsList = value;
            OnPropertyChanged(nameof(ElementsList));
        }
    }


    public void ShowWindow()
    {
        EditElementsView a = new();
        a.DataContext = this;
        a.ShowDialog();
    }

    internal void Delete()
    {
        if (SelectedItem != null)
        {
            var toRemove = _context.RPGElementDefinitions.FirstOrDefault(x => x.Id == SelectedItem.Id);
            if (toRemove != null)
            {
                _context.Remove(toRemove);
                _context.SaveChanges();
                ElementsList.Remove(SelectedItem);
                SelectedItem = null;
            }
        }
    }

    internal void CancelEdit()
    {
        CurrentlyEditingItem = null;
    }

   
}

public class ElementSelectItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ElementType { get; set; } = null!;
}
