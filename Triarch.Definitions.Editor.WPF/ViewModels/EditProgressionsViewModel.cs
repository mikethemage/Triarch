﻿using System.Collections.ObjectModel;
using System.Linq;
using Triarch.Database;
using Triarch.Database.Models.Definitions;
using Triarch.Definitions.Editor.WPF.Views;

namespace Triarch.Definitions.Editor.WPF.ViewModels;

internal class EditProgressionsViewModel : ObservableViewModel
{
    public bool EditItemShouldBeVisible
    {
        get
        {
            return CurrentlyEditingItem != null;
        }
    }

    public Progression? CurrentlyEditingItem
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

    public EditProgressionsViewModel(TriarchDbContext context, RPGSystem rPGSystem)
    {
        _context = context;
        _rPGSystem = rPGSystem;

        ProgressionsList = new(_context.Entry(_rPGSystem).Collection(x => x.Progressions).Query().Where(x => x.CustomProgression == false).OrderBy(x => x.ProgressionType).Select(x => new ProgressionSelectItem { Id = x.Id, Name = x.ProgressionType }));
    }

    public void Edit()
    {
        if (CurrentlyEditingItem == null)
        {
            if (SelectedItem != null)
            {
                CurrentlyEditingItem = _context.Progressions.FirstOrDefault(x => x.Id == SelectedItem.Id);
            }
        }
    }

    public void Create()
    {
        if (CurrentlyEditingItem == null)
        {
            CurrentlyEditingItem = new Progression
            {
                Id = 0,
                RPGSystem = _rPGSystem,
                CustomProgression = false
            };
        }
    }

    public void Save()
    {
        if (CurrentlyEditingItem != null)
        {
            if (CurrentlyEditingItem.Id == 0)
            {
                _context.Progressions.Add(CurrentlyEditingItem);
            }
            _context.SaveChanges();

            ProgressionsList = new(_context.Entry(_rPGSystem).Collection(x => x.Progressions).Query().Where(x => x.CustomProgression == false).OrderBy(x => x.ProgressionType).Select(x => new ProgressionSelectItem { Id = x.Id, Name = x.ProgressionType }));
            SelectedItem = ProgressionsList.FirstOrDefault(x => x.Id == CurrentlyEditingItem.Id);
            CurrentlyEditingItem = null;
        }
    }

    private ProgressionSelectItem? _selectedItem;

    public ProgressionSelectItem? SelectedItem
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

    private ObservableCollection<ProgressionSelectItem> _progressionsList = null!;

    private Progression? _currentlyEditingItem;

    public ObservableCollection<ProgressionSelectItem> ProgressionsList
    {
        get
        {
            return _progressionsList;
        }
        set
        {
            _progressionsList = value;
            OnPropertyChanged(nameof(ProgressionsList));
        }
    }

    public void ShowWindow()
    {
        EditProgressionsView a = new();
        a.DataContext = this;
        a.ShowDialog();
    }

    internal void Delete()
    {
        if (SelectedItem != null)
        {
            var toRemove = _context.Progressions.FirstOrDefault(x => x.Id == SelectedItem.Id);
            if (toRemove != null)
            {
                _context.Remove(toRemove);
                _context.SaveChanges();
                ProgressionsList.Remove(SelectedItem);
                SelectedItem = null;
            }
        }
    }

    internal void CancelEdit()
    {
        CurrentlyEditingItem = null;
    }

    internal void EditDefinitions()
    {
        if (SelectedItem != null)
        {
            var b = _context.Progressions.FirstOrDefault(x => x.Id == SelectedItem.Id);
            if (b != null)
            {
                var a = new EditProgressionDefinitionViewModel(_context, b);
                a.ShowWindow();
            }
        }
    }
}

public class ProgressionSelectItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
