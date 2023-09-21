using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triarch.RPGSystem.Models;

namespace Triarch.RPGSystem.Editor.WPF;

public class RPGSystemSelectViewModel : INotifyPropertyChanged
{
    public ObservableCollection<RPGSystemSelectItem> RPGSystemList
    {
        get
        {
            return rPGSystemList;
        }
        set
        {
            rPGSystemList = value;
            OnPropertyChanged(nameof(RPGSystemList));
        }
    }

    private TriarchDbContext _context = new();
    private RPGSystemSelectItem? selectedItem;
    private ObservableCollection<RPGSystemSelectItem> rPGSystemList = null!;

    public event PropertyChangedEventHandler? PropertyChanged;

    public TriarchDbContext GetDbContext() => _context;

    public RPGSystemSelectViewModel()
    {
        RPGSystemList = new ObservableCollection<RPGSystemSelectItem>(_context.RPGSystems.Select(x => new RPGSystemSelectItem { Id = x.Id, Name = x.SystemName }));
    }

    public void RequeryList()
    {
        RPGSystemList = new ObservableCollection<RPGSystemSelectItem>(_context.RPGSystems.Select(x => new RPGSystemSelectItem { Id = x.Id, Name = x.SystemName }));
    }
    private void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public RPGSystemSelectItem? SelectedItem
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

    public void Delete()
    {
        if (selectedItem != null)
        {
            var toRemove = _context.RPGSystems.FirstOrDefault(x => x.Id == selectedItem.Id);
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
