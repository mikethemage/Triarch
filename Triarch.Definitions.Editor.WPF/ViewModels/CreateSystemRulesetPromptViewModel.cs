using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Triarch.RPGSystem.Editor.WPF.Views;
using Triarch.RPGSystem.Models;

namespace Triarch.RPGSystem.Editor.WPF.ViewModels;

public class CreateSystemRulesetPromptViewModel : INotifyPropertyChanged
{
    

    public ObservableCollection<CoreRulesetSelectItem> RulesetList { get; set; }
    public TriarchDbContext GetDbContext() => _context;

    private TriarchDbContext _context = new();
    private CoreRulesetSelectItem? selectedItem = null;

    public event PropertyChangedEventHandler? PropertyChanged;

    public CoreRulesetSelectItem? SelectedItem
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

    public CoreRuleset? GetSelectedRuleset()
    {
        if (SelectedItem == null)
            return null;

        return _context.CoreRulesets.FirstOrDefault(x => x.Id == SelectedItem.Id);
    }

    private void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public void ShowWindow()
    {
        CreateSystemRulesetPromptView a = new();
        a.DataContext = this;
        a.ShowDialog();        
    }

    public CreateSystemRulesetPromptViewModel(TriarchDbContext context) 
    { 
        _context = context;
        RulesetList = new ObservableCollection<CoreRulesetSelectItem>(_context.CoreRulesets.Select(x => new CoreRulesetSelectItem { Id = x.Id, Name = x.CoreRulesetName }).OrderBy(x => x.Name));
        SelectedItem = RulesetList.FirstOrDefault();        
    }    




}

public class CoreRulesetSelectItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}