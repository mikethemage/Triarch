using System.Collections.ObjectModel;
using System.Linq;
using Triarch.Database;
using Triarch.Database.Models.Definitions;
using Triarch.Definitions.Editor.WPF.Views;

namespace Triarch.Definitions.Editor.WPF.ViewModels;

internal class EditProgressionDefinitionViewModel : ObservableViewModel
{
    public bool EditItemShouldBeVisible
    {
        get
        {
            return CurrentlyEditingItem != null;
        }
    }

    public ProgressionEntry? CurrentlyEditingItem
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

    private Progression _progression;

    public EditProgressionDefinitionViewModel(TriarchDbContext context, Progression progression)
    {
        _context = context;
        _progression = progression;

        ProgressionDefinitionsList = new(_context.Entry(progression).Collection(x => x.ProgressionEntries).Query().OrderBy(x => x.ProgressionLevel).Select(x => new ProgressionDefinitionSelectItem { Id = x.Id, Name = x.Text, Level = x.ProgressionLevel }));
    }

    public void Edit()
    {
        if (CurrentlyEditingItem == null)
        {
            if (SelectedItem != null)
            {
                CurrentlyEditingItem = _context.ProgressionEntries.FirstOrDefault(x => x.Id == SelectedItem.Id);
            }
        }
    }

    public void Create()
    {
        if (CurrentlyEditingItem == null)
        {
            int NextOrder = 0;
            if (_context.Entry(_progression).Collection(x => x.ProgressionEntries).Query().Any())
            {
                NextOrder = _context.Entry(_progression).Collection(x => x.ProgressionEntries).Query().Max(x => x.ProgressionLevel);
                NextOrder++;
            }

            CurrentlyEditingItem = new ProgressionEntry
            {
                Id = 0,
                Progression = _progression,
                ProgressionLevel = NextOrder
            };
        }
    }

    public void Save()
    {
        if (CurrentlyEditingItem != null)
        {
            if (CurrentlyEditingItem.Id == 0)
            {
                _context.ProgressionEntries.Add(CurrentlyEditingItem);
            }
            _context.SaveChanges();
            ProgressionDefinitionsList = new(_context.Entry(_progression).Collection(x => x.ProgressionEntries).Query().OrderBy(x => x.ProgressionLevel).Select(x => new ProgressionDefinitionSelectItem { Id = x.Id, Name = x.Text, Level = x.ProgressionLevel }));
            SelectedItem = ProgressionDefinitionsList.FirstOrDefault(x => x.Id == CurrentlyEditingItem.Id);
            CurrentlyEditingItem = null;
        }


    }

    private ProgressionDefinitionSelectItem? _selectedItem;

    public ProgressionDefinitionSelectItem? SelectedItem
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

    private ObservableCollection<ProgressionDefinitionSelectItem> _progressionDefinitionsList = null!;

    private ProgressionEntry? _currentlyEditingItem;

    public ObservableCollection<ProgressionDefinitionSelectItem> ProgressionDefinitionsList
    {
        get
        {
            return _progressionDefinitionsList;
        }
        set
        {
            _progressionDefinitionsList = value;
            OnPropertyChanged(nameof(ProgressionDefinitionsList));
        }
    }

    public void ShowWindow()
    {
        EditProgressionDefinitionView a = new();
        a.DataContext = this;
        a.ShowDialog();
    }

    public void MoveUp()
    {
        if (SelectedItem != null)
        {
            ProgressionDefinitionSelectItem aaa = SelectedItem;
            int currentIndex = ProgressionDefinitionsList.IndexOf(aaa);
            if (currentIndex > 0)
            {
                ProgressionDefinitionSelectItem temp = ProgressionDefinitionsList[currentIndex - 1];

                _context.ProgressionEntries.First(x => x.Id == aaa.Id).ProgressionLevel--;
                _context.ProgressionEntries.First(x => x.Id == temp.Id).ProgressionLevel++;
                _context.SaveChanges();

                ProgressionDefinitionsList[currentIndex - 1] = aaa;
                ProgressionDefinitionsList[currentIndex] = temp;
                OnPropertyChanged(nameof(ProgressionDefinitionsList));
                SelectedItem = aaa;
            }
        }
    }

    public void MoveDown()
    {
        if (SelectedItem != null)
        {
            ProgressionDefinitionSelectItem aaa = SelectedItem;
            int currentIndex = ProgressionDefinitionsList.IndexOf(aaa);
            if (currentIndex < ProgressionDefinitionsList.Count - 1)
            {
                ProgressionDefinitionSelectItem temp = ProgressionDefinitionsList[currentIndex + 1];

                _context.ProgressionEntries.First(x => x.Id == aaa.Id).ProgressionLevel++;
                _context.ProgressionEntries.First(x => x.Id == temp.Id).ProgressionLevel--;
                _context.SaveChanges();

                ProgressionDefinitionsList[currentIndex + 1] = aaa;
                ProgressionDefinitionsList[currentIndex] = temp;
                OnPropertyChanged(nameof(ProgressionDefinitionsList));
                SelectedItem = aaa;
            }
        }
    }

    internal void Delete()
    {
        if (SelectedItem != null)
        {
            ProgressionEntry? toRemove = _context.ProgressionEntries.FirstOrDefault(x => x.Id == SelectedItem.Id);
            if (toRemove != null)
            {
                int orderRemoved = toRemove.ProgressionLevel;
                _context.Remove(toRemove);

                foreach (var item in _context.ProgressionEntries.Where(x => x.Progression == _progression && x.ProgressionLevel > orderRemoved))
                {
                    item.ProgressionLevel--;
                }

                _context.SaveChanges();
                ProgressionDefinitionsList.Remove(SelectedItem);
                SelectedItem = null;
            }
        }
    }

    internal void CancelEdit()
    {
        CurrentlyEditingItem = null;
    }
}

public class ProgressionDefinitionSelectItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int Level { get; set; } = 0;
}
