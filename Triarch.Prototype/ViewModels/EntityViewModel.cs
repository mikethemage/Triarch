using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels;

public class EntityViewModel : INotifyPropertyChanged
{
    public string FileName
    {
        get
        {
            if (_filePath == "")
            {
                return "";
            }
            else
            {
                return Path.GetFileName(_filePath);
            }
        }
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private readonly RPGEntity _entity;
    private string _filePath;
    private EntityElementViewModel? _selectedElement = null;
    private ObservableCollection<GenreListItem> _genreList = new ObservableCollection<GenreListItem>();
    private GenreListItem _selectedGenre = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<GenreListItem> GenreList
    {
        get
        {
            return _genreList;
        }
        set
        {
            _genreList = value;
            OnPropertyChanged(nameof(GenreList));
        }
    }

    public GenreListItem SelectedGenre
    {
        get
        {
            return _selectedGenre;
        }
        set
        {
            _selectedGenre = value;
            OnPropertyChanged(nameof(SelectedGenre));
        }
    }

    public EntityViewModel(RPGEntity entity, string filePath = "")
    {
        _entity = entity;
        _filePath = filePath;
        EntityElements = new EntityElementsListViewModel(_entity, this);
        GenreList = new ObservableCollection<GenreListItem>(entity.RPGSystem.Genres.Select(x=>new GenreListItem { DisplayText=x.GenreName, IsSelected=false, Model=x}).ToList());
        SelectedGenre = GenreList.Where(x=>x.Model==entity.Genre).First();
        OnPropertyChanged(nameof(EntityElements));
    }

    public EntityElementsListViewModel EntityElements { get; private set; }

    public EntityElementViewModel? SelectedElement
    {
        get
        {
            return _selectedElement;
        }
        set
        {
            _selectedElement = value;
            OnPropertyChanged(nameof(SelectedElement));
        }
    }
}
