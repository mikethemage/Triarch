using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triarch.RPGSystem.Models;

namespace Triarch.RPGSystem.Editor.WPF.ViewModels;
public class LevelableViewModel : ObservableViewModel
{
    private TriarchDbContext _context;
    private LevelableDefinition _levelableDefinition;    

    //public LevelableViewModel(TriarchDbContext context) 
    //{ 
    //    _context = context;
    //    levelableDefinition = new();
    //    _context.Add(levelableDefinition);
    //}
    public LevelableViewModel(TriarchDbContext context, LevelableDefinition existingLevelableDefinition)
    {
        _context = context;
        _levelableDefinition= existingLevelableDefinition;
    }

    public int? MaxLevel
    {
        get
        {
            return _levelableDefinition.MaxLevel;
        }
        set
        {
            _levelableDefinition.MaxLevel = value;
            OnPropertyChanged(nameof(MaxLevel));
        }
    }

}
