using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using Triarch.BusinessLogic.Models.Entities;
using Triarch.BusinessLogic.Services;
using Triarch.Dtos.Entities;
using System.Text.Json.Serialization;

namespace Triarch.Prototype.ViewModels;

public class EntityEditorViewModel : ViewModelBase, IPageViewModel
{
    public EntityEditorViewModel(RPGEntity entity, string filePath = "")
    {
        _entity = entity;
        _filePath = filePath;
        EntityElements = new EntityElementsListViewModel(_entity, this);
        GenreList = new ObservableCollection<GenreListItem>(entity.RPGSystem.Genres.Select(x => new GenreListItem { DisplayText = x.GenreName, IsSelected = false, Model = x }).ToList());
        _selectedGenre = GenreList.Where(x => x.Model == entity.Genre).First();
        OnPropertyChanged(nameof(EntityElements));
        AddCommand = new RelayCommand(Add, CanAdd);
        DeleteCommand = new RelayCommand(Delete, CanDelete);
        MoveUpCommand = new RelayCommand(MoveUp, CanMoveUp);
        MoveDownCommand = new RelayCommand(MoveDown, CanMoveDown);
        BackCommand = new RelayCommand(Back, CanBack);
        SaveCommand = new RelayCommand(Save, CanSave);
        SaveAsCommand = new RelayCommand(SaveAs, CanSaveAs);
        ExportTextCommand = new RelayCommand(ExportText, CanExportText);
    }

    private bool CanExportText()
    {
        return true;
    }

    private void ExportText()
    {
        SaveFileDialog exportFileDialog = new SaveFileDialog
        {
            RestoreDirectory = false,
            Filter = "Text" + " Files(*.txt)|*.txt|All Files (*.*)|*.*",
            FilterIndex = 1
        };

        if (exportFileDialog.ShowDialog() ?? false)
        {
            TextExporter textExporter = new TextExporter();
            string exportedText = textExporter.Export(_entity);
            File.WriteAllText(exportFileDialog.FileName, exportedText);
        }
    }

    public required MainWindowViewModel Parent { get; set; }

    public bool ChangesSaved { get; set; } = false;

    public RelayCommand? SaveCommand { get; set; }
    public RelayCommand? SaveAsCommand { get; set; }
    public RelayCommand? BackCommand { get; set; }
    public RelayCommand? AddCommand { get; private set; }

    public RelayCommand? ExportTextCommand { get; set; }

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

    private readonly RPGEntity _entity;
    private string _filePath;
    private EntityElementViewModel? _selectedElement = null;
    private ObservableCollection<GenreListItem> _genreList = new ObservableCollection<GenreListItem>();
    private GenreListItem _selectedGenre = null!;

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
            _entity.Genre = _selectedGenre.Model;
            OnPropertyChanged(nameof(SelectedGenre));
            ChangesSaved = false;
            if (SelectedElement?.LevelableData != null)
            {
                SelectedElement.LevelableData.RefreshProperties();
            }
        }
    }    

    public void SaveAs()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            RestoreDirectory = false,
            Filter = "Triarch" + " Files(*.json)|*.json|All Files (*.*)|*.*",
            FilterIndex = 1
        };

        if (saveFileDialog.ShowDialog() ?? false)
        {
            WriteEntityToFile(saveFileDialog.FileName);
            _filePath = saveFileDialog.FileName;
        }
    }

    public void Save()
    {
        if (_filePath == "")
        {
            SaveAs();
        }
        else
        {
            WriteEntityToFile(_filePath);
        }
    }

    private void WriteEntityToFile(string filename)
    {
        RPGSystemProvider rPGSystemProvider = new RPGSystemProvider();
        rPGSystemProvider.AddSystem(_entity.RPGSystem.SystemName, _entity.RPGSystem);
        RPGEntityMapper rPGEntityMapper = new RPGEntityMapper(rPGSystemProvider);
        EntityDto entityDto = rPGEntityMapper.Serialize(_entity);
        File.WriteAllText(filename, JsonSerializer.Serialize(entityDto, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));
        ChangesSaved = true;
    }

    public bool CanSave()
    {
        return true;
    }

    public bool CanSaveAs()
    {
        return true;
    }

    public void Back()
    {
        if (!ChangesSaved)
        {
            if (MessageBox.Show("Save changes before closing?", "Unsaved Changes", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Save();
                if (!ChangesSaved)
                {
                    return;
                }
            }
        }
        Parent.CurrentPage = new MainMenuViewModel { Parent = Parent };
    }

    public bool CanBack()
    {
        return true;
    }

    public RelayCommand? DeleteCommand
    {
        get; private set;
    }
    public void Delete()
    {
        if (SelectedElement != null && SelectedElement.Element.Parent != null)
        {
            EntityElementListItemViewModel parent = EntityElements.ElementList[SelectedElement.Element.Parent];

            EntityController entityController = new EntityController();
            Stack<RPGElement> deletedElements = entityController.DeleteElement(SelectedElement.Element);
            while (deletedElements.Count > 0)
            {
                RPGElement toRemove = deletedElements.Pop();
                EntityElements.ElementList.Remove(toRemove);
            }
            int previousIndex = parent.Children.IndexOf(EntityElements.Selected);
            parent.Children.Remove(EntityElements.Selected);
            ChangesSaved = false;

            if (parent.Children.Count == 0)
            {
                parent.IsSelected = true;
            }
            else
            {
                if (previousIndex < parent.Children.Count)
                {
                    parent.Children[previousIndex].IsSelected = true;
                }
                else
                {
                    parent.Children[parent.Children.Count - 1].IsSelected = true;
                }
            }
            MoveUpCommand?.RaiseCanExecuteChanged();
            MoveDownCommand?.RaiseCanExecuteChanged();
        }
    }

    public bool CanDelete()
    {
        if (SelectedElement == null)
        {
            return false;
        }
        else
        {
            return SelectedElement.Element.CanDelete();
        }
    }

    public void Add()
    {
        if (SelectedElement?.AllowedChildrenList.SelectedChild != null)
        {
            EntityController entityController = new EntityController();
            RPGElement addedElement = entityController.AddElement(SelectedElement.Element, SelectedElement.AllowedChildrenList.SelectedChild.Model);
            EntityElementListItemViewModel addedElementViewModel = new(addedElement, EntityElements);
            EntityElements.Selected.Children.Add(addedElementViewModel);
            EntityElements.ElementList.Add(addedElement, addedElementViewModel);
            ChangesSaved = false;

            if (SelectedElement?.CharacterData != null)
            {
                SelectedElement.CharacterData.RefreshProperties();
            }

            if (SelectedElement?.LevelableData != null)
            {
                SelectedElement.LevelableData.RefreshProperties();
            }
        }
    }

    public bool CanAdd()
    {
        if (SelectedElement?.AllowedChildrenList.SelectedChild != null)
        {
            return true;
        }
        else
        {
            return false;
        }
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
            if (_selectedElement != null)
            {
                _selectedElement.AllowedChildrenList.PropertyChanged -= OnAllowedChildrenListPropertyChanged;
            }
            _selectedElement = value;
            OnPropertyChanged(nameof(SelectedElement));
            if (_selectedElement != null)
            {
                _selectedElement.AllowedChildrenList.PropertyChanged += OnAllowedChildrenListPropertyChanged;
            }

            AddCommand?.RaiseCanExecuteChanged(); // Ensure CanAdd is re-evaluated when SelectedElement changes
            DeleteCommand?.RaiseCanExecuteChanged();
            MoveUpCommand?.RaiseCanExecuteChanged();
            MoveDownCommand?.RaiseCanExecuteChanged();
        }
    }

    private void OnAllowedChildrenListPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AllowedChildrenViewModel.SelectedChild))
        {
            AddCommand?.RaiseCanExecuteChanged();
        }
    }

    public RelayCommand? MoveUpCommand
    {
        get; private set;
    }
    public RelayCommand? MoveDownCommand
    {
        get; private set;
    }

    public void MoveUp()
    {
        if (SelectedElement != null && SelectedElement.Element.Parent != null)
        {
            EntityController entityController = new EntityController();
            if (entityController.MoveUpElement(SelectedElement.Element))
            {
                var parent = EntityElements.ElementList[SelectedElement.Element.Parent];
                var child = EntityElements.ElementList[SelectedElement.Element];

                int currentIndex = parent.Children.IndexOf(child);
                int previousIndex = currentIndex - 1;
                var previous = parent.Children[previousIndex];

                parent.Children[currentIndex] = previous;
                parent.Children[previousIndex] = child;

                ChangesSaved = false;

                MoveUpCommand?.RaiseCanExecuteChanged();
                MoveDownCommand?.RaiseCanExecuteChanged();
            }
        }
    }

    public void MoveDown()
    {
        if (SelectedElement != null && SelectedElement.Element.Parent != null)
        {
            EntityController entityController = new EntityController();
            if (entityController.MoveDownElement(SelectedElement.Element))
            {
                var parent = EntityElements.ElementList[SelectedElement.Element.Parent];
                var child = EntityElements.ElementList[SelectedElement.Element];

                int currentIndex = parent.Children.IndexOf(child);
                int nextIndex = currentIndex + 1;
                var next = parent.Children[nextIndex];

                parent.Children[currentIndex] = next;
                parent.Children[nextIndex] = child;
                ChangesSaved = false;

                MoveDownCommand?.RaiseCanExecuteChanged();
                MoveUpCommand?.RaiseCanExecuteChanged();
            }
        }
    }

    public bool CanMoveUp()
    {
        if (SelectedElement == null)
        {
            return false;
        }
        return SelectedElement.Element.CanMoveUp();
    }

    public bool CanMoveDown()
    {
        if (SelectedElement == null)
        {
            return false;
        }
        return SelectedElement.Element.CanMoveDown();
    }
}
