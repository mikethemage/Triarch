﻿using System.Collections.ObjectModel;
using System.Linq;
using Triarch.Database;
using Triarch.Database.Models.Definitions;
using Triarch.Definitions.Editor.WPF.Views;

namespace Triarch.Definitions.Editor.WPF.ViewModels;
internal class EditTypesViewModel : ObservableViewModel
{
    public bool EditItemShouldBeVisible
    {
        get
        {
            return CurrentlyEditingItem != null;
        }
    }

    public RPGElementType? CurrentlyEditingItem
    {
        get
        {
            return _currentlyEditingItem;
        }
        set
        {
            _currentlyEditingItem = value;
            OnPropertyChanged(nameof(CurrentlyEditingItem));
            OnPropertyChanged(nameof(EditItemShouldBeVisible));
        }
    }

    private TriarchDbContext _context;

    private RPGSystem _rPGSystem;

    public EditTypesViewModel(TriarchDbContext context, RPGSystem rPGSystem)
    {
        _context = context;
        _rPGSystem = rPGSystem;

        TypesList = new(_context.Entry(_rPGSystem).Collection(x => x.RPGElementTypes).Query().OrderBy(x => x.TypeOrder).Select(x => new RPGTypeSelectItem { Id = x.Id, Name = x.TypeName }));
    }

    public void Edit()
    {
        if (CurrentlyEditingItem == null)
        {
            if (SelectedItem != null)
            {
                CurrentlyEditingItem = _context.RPGElementTypes.FirstOrDefault(x => x.Id == SelectedItem.Id);
            }
        }
    }

    public void Create()
    {
        if (CurrentlyEditingItem == null)
        {
            int NextOrder = 0;
            if (_context.Entry(_rPGSystem).Collection(x => x.RPGElementTypes).Query().Any())
            {
                NextOrder = _context.Entry(_rPGSystem).Collection(x => x.RPGElementTypes).Query().Max(x => x.TypeOrder);
            }
            NextOrder++;

            CurrentlyEditingItem = new RPGElementType
            {
                Id = 0,
                RPGSystem = _rPGSystem,
                TypeOrder = NextOrder
            };
        }
    }

    public void Save()
    {
        if (CurrentlyEditingItem != null)
        {
            if (CurrentlyEditingItem.Id == 0)
            {
                _context.RPGElementTypes.Add(CurrentlyEditingItem);
            }
            _context.SaveChanges();
            TypesList = new(_context.Entry(_rPGSystem).Collection(x => x.RPGElementTypes).Query().OrderBy(x => x.TypeOrder).Select(x => new RPGTypeSelectItem { Id = x.Id, Name = x.TypeName }));
            SelectedItem = TypesList.FirstOrDefault(x => x.Id == CurrentlyEditingItem.Id);
            CurrentlyEditingItem = null;
        }
    }

    private RPGTypeSelectItem? _selectedItem;

    public RPGTypeSelectItem? SelectedItem
    {
        get
        {
            return _selectedItem;
        }
        set
        {
            _selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));
        }
    }

    private ObservableCollection<RPGTypeSelectItem> _typesList = null!;

    private RPGElementType? _currentlyEditingItem;

    public ObservableCollection<RPGTypeSelectItem> TypesList
    {
        get
        {
            return _typesList;
        }
        set
        {
            _typesList = value;
            OnPropertyChanged(nameof(TypesList));
        }
    }

    public void ShowWindow()
    {
        EditTypesView a = new();
        a.DataContext = this;
        a.ShowDialog();
    }

    public void MoveUp()
    {
        if (SelectedItem != null)
        {
            RPGTypeSelectItem aaa = SelectedItem;
            int currentIndex = TypesList.IndexOf(aaa);
            if (currentIndex > 0)
            {
                RPGTypeSelectItem temp = TypesList[currentIndex - 1];

                _context.RPGElementTypes.First(x => x.Id == aaa.Id).TypeOrder--;
                _context.RPGElementTypes.First(x => x.Id == temp.Id).TypeOrder++;
                _context.SaveChanges();

                TypesList[currentIndex - 1] = aaa;
                TypesList[currentIndex] = temp;
                OnPropertyChanged(nameof(TypesList));
                SelectedItem = aaa;
            }
        }
    }

    public void MoveDown()
    {
        if (SelectedItem != null)
        {
            RPGTypeSelectItem aaa = SelectedItem;
            int currentIndex = TypesList.IndexOf(aaa);
            if (currentIndex < TypesList.Count - 1)
            {
                RPGTypeSelectItem temp = TypesList[currentIndex + 1];

                _context.RPGElementTypes.First(x => x.Id == aaa.Id).TypeOrder++;
                _context.RPGElementTypes.First(x => x.Id == temp.Id).TypeOrder--;
                _context.SaveChanges();

                TypesList[currentIndex + 1] = aaa;
                TypesList[currentIndex] = temp;
                OnPropertyChanged(nameof(TypesList));
                SelectedItem = aaa;
            }
        }
    }

    internal void Delete()
    {
        if (SelectedItem != null)
        {
            var toRemove = _context.RPGElementTypes.FirstOrDefault(x => x.Id == SelectedItem.Id);
            if (toRemove != null)
            {
                int orderRemoved = toRemove.TypeOrder;
                _context.Remove(toRemove);

                foreach (var item in _context.RPGElementTypes.Where(x => x.RPGSystem == _rPGSystem && x.TypeOrder > orderRemoved))
                {
                    item.TypeOrder--;
                }
                _context.SaveChanges();
                TypesList.Remove(SelectedItem);
                SelectedItem = null;
            }
        }
    }

    internal void CancelEdit()
    {
        CurrentlyEditingItem = null;
    }
}

public class RPGTypeSelectItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
