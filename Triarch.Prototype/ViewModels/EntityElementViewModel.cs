using System.ComponentModel;
using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;
using Triarch.BusinessLogic.Services;

namespace Triarch.Prototype.ViewModels;

public class EntityElementViewModel : INotifyPropertyChanged
{
    public EntityElementViewModel(RPGElement element)
    {
        _element = element;

        if (element is Levelable levelable)
        {
            LevelableData = new LevelableDataViewModel(levelable);
            if (element.AssociatedDefinition is LevelableDefinition levelableDefinition)
            {
                if (levelableDefinition.Variants != null && levelableDefinition.Variants.Count > 0)
                {
                    VariantList = new VariantListViewModel(levelable);
                }
            }
        }
        if (element is Character character)
        {
            CharacterData = new CharacterDataViewModel(character);
        }

        AllowedChildrenList = new AllowedChildrenViewModel(element.AssociatedDefinition.AllowedChildren);

        MoveUpCommand = new RelayCommand(MoveUp, _element.CanMoveUp);
        MoveDownCommand = new RelayCommand(MoveDown, _element.CanMoveDown);
        DeleteCommand = new RelayCommand(Delete, _element.CanDelete);
        AddCommand = new RelayCommand(Add, AllowedChildrenList.CanAdd);
    }

    public RelayCommand? DeleteCommand
    {
        get; private set;
    }
    public RelayCommand? MoveUpCommand
    {
        get; private set;
    }
    public RelayCommand? MoveDownCommand
    {
        get; private set;
    }
    public RelayCommand? AddCommand
    {
        get; private set;
    }

    public void MoveUp()
    {
        EntityController entityController = new EntityController();
        if(entityController.MoveUpElement(_element))
        {

        }
    }
    public void MoveDown()
    {

    }
    public void Delete()
    {

    }
    public void Add()
    {

    }

    

    private readonly RPGElement _element;
    private VariantListViewModel? _variantList = null;
    private AllowedChildrenViewModel _allowedChildrenList = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public VariantListViewModel? VariantList
    {
        get
        {
            return _variantList;
        }
        private set
        {
            _variantList = value;
            OnPropertyChanged(nameof(VariantList));
        }
    }
    public AllowedChildrenViewModel AllowedChildrenList
    {
        get
        {
            return _allowedChildrenList;
        }
        set
        {
            _allowedChildrenList = value;
            OnPropertyChanged(nameof(AllowedChildrenList));
        }
    }
    public CharacterDataViewModel? CharacterData { get; set; } = null;
    public LevelableDataViewModel? LevelableData { get; set; } = null;

    public string Notes
    {
        get { return _element.Notes; }
        set
        {
            _element.Notes = value;
        }
    }
}