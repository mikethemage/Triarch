using System.Collections.ObjectModel;
using System.Linq;
using Triarch.Definitions.Editor.WPF.Views;
using Triarch.Database;
using Triarch.Database.Models.Definitions;

namespace Triarch.Definitions.Editor.WPF.ViewModels;

internal class EditGenresViewModel : ObservableViewModel
{  
    public bool EditItemShouldBeVisible
    {
        get
        {
            return CurrentlyEditingItem != null;
        }
    }

    public Genre? CurrentlyEditingItem
    {
        get
        {
            return currentlyEditingItem;
        }
        set
        {
            currentlyEditingItem = value;
            OnPropertyChanged(nameof(CurrentlyEditingItem));
            OnPropertyChanged(nameof(EditItemShouldBeVisible));
        }
    }

    private TriarchDbContext _context;
    
    private RPGSystem _rPGSystem;

    public EditGenresViewModel(TriarchDbContext context, RPGSystem rPGSystem)
    {
        _context = context;
        _rPGSystem = rPGSystem;        

        GenresList = new (_context.Entry(_rPGSystem).Collection(x => x.Genres).Query().OrderBy(x=>x.GenreOrder).Select(x=> new RPGGenreSelectItem { Id = x.Id, Name=x.GenreName}));
    }

    public void Edit()
    {
        if (CurrentlyEditingItem == null)
        {
            if (SelectedItem != null)
            {
                CurrentlyEditingItem = _context.Genres.FirstOrDefault(x => x.Id == SelectedItem.Id);
            }
        }              
    }

    public void Create()
    {
        if (CurrentlyEditingItem == null)
        {
            int NextOrder = 0;
            if(_context.Entry(_rPGSystem).Collection(x => x.Genres).Query().Any())
            {
                NextOrder = _context.Entry(_rPGSystem).Collection(x => x.Genres).Query().Max(x => x.GenreOrder);
            }
            NextOrder++;

            CurrentlyEditingItem = new Genre
            {
                Id = 0,
                RPGSystem = _rPGSystem,
                GenreOrder = NextOrder
            };
        }
    }

    public void Save()
    {
        if (CurrentlyEditingItem != null)
        {
            if(CurrentlyEditingItem.Id == 0)
            {
                _context.Genres.Add(CurrentlyEditingItem);
            }
            _context.SaveChanges();
            GenresList = new(_context.Entry(_rPGSystem).Collection(x => x.Genres).Query().OrderBy(x => x.GenreOrder).Select(x => new RPGGenreSelectItem { Id = x.Id, Name = x.GenreName }));
            SelectedItem = GenresList.FirstOrDefault(x => x.Id == CurrentlyEditingItem.Id);
            CurrentlyEditingItem = null;
        }
    }

    private RPGGenreSelectItem? selectedItem;

    public RPGGenreSelectItem? SelectedItem
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

    private ObservableCollection<RPGGenreSelectItem> genresList = null!;
    
    private Genre? currentlyEditingItem;

    public ObservableCollection<RPGGenreSelectItem> GenresList
    {
        get
        {
            return genresList;
        }
        set
        {
            genresList = value;
            OnPropertyChanged(nameof(GenresList));
        }
    }

    public void ShowWindow()
    {
        EditGenresView a = new();
        a.DataContext = this;
        a.ShowDialog();
    }

    public void MoveUp()
    {
        if(SelectedItem != null)
        {
            RPGGenreSelectItem aaa = SelectedItem;
            int currentIndex = GenresList.IndexOf(aaa);
            if (currentIndex > 0)
            {
                RPGGenreSelectItem temp = GenresList[currentIndex - 1];

                _context.Genres.First(x => x.Id == aaa.Id).GenreOrder--;
                _context.Genres.First(x => x.Id == temp.Id).GenreOrder++;
                _context.SaveChanges();
                
                
                GenresList[currentIndex - 1]= aaa;
                GenresList[currentIndex] = temp;
                OnPropertyChanged(nameof(GenresList));
                SelectedItem = aaa;
            }
        }
    }

    public void MoveDown()
    {
        if (SelectedItem != null)
        {
            RPGGenreSelectItem aaa = SelectedItem;
            int currentIndex = GenresList.IndexOf(aaa);
            if (currentIndex < GenresList.Count - 1)
            {
                RPGGenreSelectItem temp = GenresList[currentIndex + 1];

                _context.Genres.First(x => x.Id == aaa.Id).GenreOrder++;
                _context.Genres.First(x => x.Id == temp.Id).GenreOrder--;
                _context.SaveChanges();

                GenresList[currentIndex + 1] = aaa;
                GenresList[currentIndex] = temp;
                OnPropertyChanged(nameof(GenresList));
                SelectedItem = aaa;
            }
        }
    }

    internal void Delete()
    {
        if (SelectedItem != null)
        {
            Genre? toRemove = _context.Genres.FirstOrDefault(x => x.Id == SelectedItem.Id);
            if(toRemove != null)
            {
                int orderRemoved = toRemove.GenreOrder;
                _context.Remove(toRemove);

                foreach(var item in _context.Genres.Where(x => x.RPGSystem==_rPGSystem && x.GenreOrder > orderRemoved))
                {
                    item.GenreOrder--;
                }

                _context.SaveChanges();
                GenresList.Remove(SelectedItem);
                SelectedItem = null;
            }               
        }        
    }

    internal void CancelEdit()
    {
        CurrentlyEditingItem=null;
    }
}

public class RPGGenreSelectItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
