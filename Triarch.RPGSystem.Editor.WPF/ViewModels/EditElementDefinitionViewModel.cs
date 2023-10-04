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
internal class EditElementDefinitionViewModel : ObservableViewModel
{   
    private RPGElementDefinition _elementDefinition;

    private TriarchDbContext _context;
    private bool saved;
    private bool levelable;
    private LevelableViewModel? levelableData;

    public LevelableViewModel? LevelableData
    {
        get
        {
            return levelableData;
        }
        set
        {
            levelableData = value;
            OnPropertyChanged(nameof(LevelableData));
        }
    }

    public EditElementDefinitionViewModel(TriarchDbContext context, RPGElementDefinition existingElementDefinition)
    {
        _context = context;
        _elementDefinition = existingElementDefinition;
        TypeList = new ObservableCollection<RPGElementType>(_context.RPGElementTypes.Where(x=>x.RPGSystem==_elementDefinition.RPGSystem).OrderBy(x=>x.TypeName));
        
        if(_elementDefinition.LevelableData != null)
        {
            LevelableData = new(_context, _elementDefinition.LevelableData);
            Levelable = true;
        }
        else
        {
            LevelableData=null;
            Levelable = false;
        }

        Saved = true;
    }

    public EditElementDefinitionViewModel(TriarchDbContext context, Models.RPGSystem rPGSystem)
    {
        _context = context;
        _elementDefinition = new();
        _elementDefinition.RPGSystem = rPGSystem;
        TypeList = new ObservableCollection<RPGElementType>(_context.RPGElementTypes.Where(x => x.RPGSystem == _elementDefinition.RPGSystem).OrderBy(x => x.TypeName));
        LevelableData = null;
        _context.Add(_elementDefinition);
        Saved = false;
    }

    public string Name
    {
        get
        {
            return _elementDefinition.ElementName;
        }
        set
        {
            _elementDefinition.ElementName = value;
            OnPropertyChanged(nameof(Name));
            Saved = false;
        }
    }

    public string Description
    {
        get
        {
            return _elementDefinition.Description ?? "";
        }
        set
        {
            _elementDefinition.Description = value; 
            OnPropertyChanged(nameof(Description));
            Saved = false;
        }
    }

    public string Stat
    {
        get
        {
            return _elementDefinition.Stat ?? "";
        }
        set
        { 
            _elementDefinition.Stat = value; 
            OnPropertyChanged(nameof(Stat));
            Saved = false;
        }
    }

    public string PageNumbers
    {
        get
        {
            return _elementDefinition.PageNumbers ?? "";
        }
        set
        {
            _elementDefinition.PageNumbers= value;
            OnPropertyChanged(nameof(PageNumbers));
            Saved = false;
        }
    }

    public bool Human
    {
        get
        { return _elementDefinition.Human; }    
        set
        {
            _elementDefinition.Human = value;
            OnPropertyChanged(nameof(Human));
            Saved = false;
        }
    }

    public bool Levelable
    {
        get
        {
            return levelable;
        }
        set
        {
            levelable = value;

            if(value==true && LevelableData==null)
            {
                if(_elementDefinition.LevelableData==null)
                {
                    _elementDefinition.LevelableData = new();
                    _context.Add(_elementDefinition.LevelableData);                    
                }
                
                LevelableData = new(_context, _elementDefinition.LevelableData);
            }

            OnPropertyChanged(nameof(Levelable));
        }
    }

    public ObservableCollection<string> FreebiesList
    {
        get
        {
            if(_elementDefinition.Freebies == null )
            {
                return new ObservableCollection<string>();
            }
            else
            {
                return new ObservableCollection<string>(_elementDefinition.Freebies.Select(x => x.FreebieElementDefinition.ElementName));
            }
            
        }        
    }

    public bool PointsContainer
    {
        get
        {
            return _elementDefinition.PointsContainerScale != null;
        }
        set
        {
            OnPropertyChanged(nameof(PointsContainer));
        }
    }

    public ObservableCollection<RPGElementType> TypeList { get; set; }

    public RPGElementType SelectedType
    {
        get
        {
            return _elementDefinition.ElementType;
        }
        set
        {
            _elementDefinition.ElementType = value;
            OnPropertyChanged(nameof(SelectedType));
        }
    }

    public void ShowWindow()
    {
        EditElementDefinitionView a = new();
        a.DataContext = this;
        a.ShowDialog();
    }

    public void Save()
    {
        if (Saved == false && !string.IsNullOrEmpty(Name) && SelectedType!=null)
        {
            if(!Levelable)
            {
                if (_elementDefinition.LevelableData != null)
                {
                    _context.Remove(_elementDefinition.LevelableData);
                }
                _elementDefinition.LevelableData = null;
                LevelableData = null;
            }

            _context.SaveChanges();
            Saved = true;
        }
    }

    internal void EditCustomProgression()
    {
        throw new NotImplementedException();
    }

    public bool Saved
    {
        get
        {
            return saved;
        }
        set
        {
            saved = value;
            OnPropertyChanged(nameof(Saved));
        }
    }
}
