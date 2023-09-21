using Microsoft.EntityFrameworkCore;
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
public class EditSystemViewModel : INotifyPropertyChanged
{
    private TriarchDbContext _context;
    private bool saved = false;
    public bool Saved
    {
        get
        {
            return saved;
        }
        private set
        {
            saved = value;
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
        rPGSystem = _context.RPGSystems.Include(x => x.Ruleset).FirstOrDefault(x => x.Id == existingRPGSystemId)!;
        Saved=true;
    }

    public EditSystemViewModel(TriarchDbContext context, CoreRuleset createFromCoreRuleset)
    {
        _context = context;
        rPGSystem = new Models.RPGSystem
        {
            Ruleset = createFromCoreRuleset
        };
        OnPropertyChanged(nameof(CoreRuleset));
        _context.Add(rPGSystem);
        Saved = false;
    }

    private Models.RPGSystem rPGSystem = null!;

    
    public void CreateEditTypes()
    {
        if(saved)
        {
            EditTypesViewModel a = new(_context, rPGSystem);
            a.ShowWindow();
        }
    }

    public void CreateEditElements()
    {
        if (saved)
        {
            EditElementsViewModel a = new(_context, rPGSystem);
            a.ShowWindow();
        }
    }

    public void CreateEditGenres()
    {
        if (saved)
        {
            EditGenresViewModel a = new(_context, rPGSystem);
            a.ShowWindow();
        }
    }

    public void CreateEditProgressions()
    {
        if (saved)
        {
            EditProgressionsViewModel a = new(_context, rPGSystem);
            a.ShowWindow();
        }
    }


    public string SystemName
    {
        get
        {
            return rPGSystem.SystemName;
        }
        set
        {
            if (rPGSystem.SystemName != value)
            {
                rPGSystem.SystemName = value;
                OnPropertyChanged(nameof(SystemName));
                Saved = false;
            }
        }
    }

    public string CoreRuleset
    {
        get
        {
            return rPGSystem.Ruleset.CoreRulesetName;
        }        
    }

    public string Description
    {
        get
        {
            return rPGSystem.DescriptiveName ?? "";
        }
        set
        {
            rPGSystem.DescriptiveName = string.IsNullOrWhiteSpace(value) ? null : value;
            OnPropertyChanged(nameof(Description));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public void Save()
    {
        _context.SaveChanges();
        Saved = true;
    }
}
