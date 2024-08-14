using System.Collections.ObjectModel;
using System.Linq;
using Triarch.Database;
using Triarch.Database.Models.Definitions;
using Triarch.Definitions.Editor.WPF.Views;

namespace Triarch.Definitions.Editor.WPF.ViewModels;

public class CreateSystemRulesetPromptViewModel : ObservableViewModel
{
    private TriarchDbContext _context = new();
    private CoreRulesetSelectItem? _selectedItem = null;

    public ObservableCollection<CoreRulesetSelectItem> RulesetList { get; set; }

    public TriarchDbContext GetDbContext() => _context;

    public CoreRulesetSelectItem? SelectedItem
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

    public CoreRuleset? GetSelectedRuleset()
    {
        if (SelectedItem == null)
            return null;

        return _context.CoreRulesets.FirstOrDefault(x => x.Id == SelectedItem.Id);
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