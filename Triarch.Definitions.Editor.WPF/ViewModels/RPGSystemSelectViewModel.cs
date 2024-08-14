using System.Collections.ObjectModel;
using System.Linq;
using Triarch.Database;

namespace Triarch.Definitions.Editor.WPF.ViewModels;

public class RPGSystemSelectViewModel : ObservableViewModel
{
    public ObservableCollection<RPGSystemSelectItem> RPGSystemList
    {
        get
        {
            return _rPGSystemList;
        }
        set
        {
            _rPGSystemList = value;
            OnPropertyChanged(nameof(RPGSystemList));
        }
    }

    private TriarchDbContext _context = new();

    private RPGSystemSelectItem? _selectedItem;

    private ObservableCollection<RPGSystemSelectItem> _rPGSystemList = null!;

    public TriarchDbContext GetDbContext() => _context;

    public RPGSystemSelectViewModel()
    {
        RPGSystemList = new ObservableCollection<RPGSystemSelectItem>(_context.RPGSystems.Select(x => new RPGSystemSelectItem { Id = x.Id, Name = x.SystemName }));
    }

    public void RequeryList()
    {
        RPGSystemList = new ObservableCollection<RPGSystemSelectItem>(_context.RPGSystems.Select(x => new RPGSystemSelectItem { Id = x.Id, Name = x.SystemName }));
    }

    public RPGSystemSelectItem? SelectedItem
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

    public void Delete()
    {
        if (_selectedItem != null)
        {
            var toRemove = _context.RPGSystems.FirstOrDefault(x => x.Id == _selectedItem.Id);
            if (toRemove != null)
            {
                _context.Remove(toRemove);
                _context.SaveChanges();
                RequeryList();
            }
        }
    }
}

public class RPGSystemSelectItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
