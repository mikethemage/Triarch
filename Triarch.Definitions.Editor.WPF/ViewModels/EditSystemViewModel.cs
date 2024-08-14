using Microsoft.EntityFrameworkCore;
using System.Linq;
using Triarch.Database;
using Triarch.Database.Models.Definitions;
using Triarch.Definitions.Editor.WPF.Views;

namespace Triarch.Definitions.Editor.WPF.ViewModels;
public class EditSystemViewModel : ObservableViewModel
{
    private TriarchDbContext _context;

    private bool _saved = false;

    public bool Saved
    {
        get
        {
            return _saved;
        }
        private set
        {
            _saved = value;
            OnPropertyChanged(nameof(Saved));
        }
    }

    public void ShowWindow()
    {
        EditSystemView a = new();
        a.DataContext = this;
        a.ShowDialog();
    }

    public TriarchDbContext GetDbContext() => _context;

    public EditSystemViewModel(TriarchDbContext context, int existingRPGSystemId)
    {
        _context = context;
        _rPGSystem = _context.RPGSystems.Include(x => x.Ruleset).FirstOrDefault(x => x.Id == existingRPGSystemId)!;
        Saved = true;
    }

    public EditSystemViewModel(TriarchDbContext context, CoreRuleset createFromCoreRuleset)
    {
        _context = context;
        _rPGSystem = new RPGSystem
        {
            Ruleset = createFromCoreRuleset
        };
        OnPropertyChanged(nameof(CoreRuleset));
        _context.Add(_rPGSystem);
        Saved = false;
    }

    private RPGSystem _rPGSystem = null!;

    public void CreateEditTypes()
    {
        if (_saved)
        {
            EditTypesViewModel a = new(_context, _rPGSystem);
            a.ShowWindow();
        }
    }

    public void CreateEditElements()
    {
        if (_saved)
        {
            EditElementsViewModel a = new(_context, _rPGSystem);
            a.ShowWindow();
        }
    }

    public void CreateEditGenres()
    {
        if (_saved)
        {
            EditGenresViewModel a = new(_context, _rPGSystem);
            a.ShowWindow();
        }
    }

    public void CreateEditProgressions()
    {
        if (_saved)
        {
            EditProgressionsViewModel a = new(_context, _rPGSystem);
            a.ShowWindow();
        }
    }

    public string SystemName
    {
        get
        {
            return _rPGSystem.SystemName;
        }
        set
        {
            if (_rPGSystem.SystemName != value)
            {
                _rPGSystem.SystemName = value;
                OnPropertyChanged(nameof(SystemName));
                Saved = false;
            }
        }
    }

    public string CoreRuleset
    {
        get
        {
            return _rPGSystem.Ruleset.CoreRulesetName;
        }
    }

    public string Description
    {
        get
        {
            return _rPGSystem.DescriptiveName ?? "";
        }
        set
        {
            _rPGSystem.DescriptiveName = string.IsNullOrWhiteSpace(value) ? null : value;
            OnPropertyChanged(nameof(Description));
        }
    }

    // ... (existing code)

    private bool _isFormLocked = false;

    public bool IsFormLocked
    {
        get => _isFormLocked;
        private set
        {
            _isFormLocked = value;
            OnPropertyChanged(nameof(IsFormLocked));
        }
    }

    public bool SaveButtonEnabled => !IsFormLocked;

    public void LockForm()
    {
        IsFormLocked = true;
        OnPropertyChanged(nameof(SaveButtonEnabled));
    }

    public void UnlockForm()
    {
        IsFormLocked = false;
        OnPropertyChanged(nameof(SaveButtonEnabled));
    }

    // Modify the Save method to lock the form after saving
    public void Save()
    {
        if (!IsFormLocked && !string.IsNullOrEmpty(SystemName))
        {
            _context.SaveChanges();
            Saved = true;
            LockForm(); // Lock the form after saving
        }
    }

    // ... (existing code)


    //public void Save()
    //{
    //    if (Saved == false && !string.IsNullOrEmpty(SystemName))
    //    {
    //        _context.SaveChanges();
    //        Saved = true;
    //    }
    //}
}
