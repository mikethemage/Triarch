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
internal class EditElementDefinitionViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private RPGElementDefinition _elementDefinition;
    private TriarchDbContext _context;
    

    public EditElementDefinitionViewModel(TriarchDbContext context, RPGElementDefinition existingElementDefinition)
    {
        _context = context;
        _elementDefinition = existingElementDefinition;
        TypeList = new ObservableCollection<RPGElementType>(_context.RPGElementTypes.Where(x=>x.RPGSystem==_elementDefinition.RPGSystem).OrderBy(x=>x.TypeName));
    }

    public EditElementDefinitionViewModel(TriarchDbContext context, Models.RPGSystem rPGSystem)
    {
        _context = context;
        _elementDefinition = new();
        _elementDefinition.RPGSystem = rPGSystem;
        TypeList = new ObservableCollection<RPGElementType>(_context.RPGElementTypes.Where(x => x.RPGSystem == _elementDefinition.RPGSystem).OrderBy(x => x.TypeName));
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

    private void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
