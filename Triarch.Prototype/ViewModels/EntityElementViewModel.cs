using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels;

public class EntityElementViewModel : ViewModelBase
{
    private readonly EntityEditorViewModel _parent;
    public EntityElementViewModel(RPGElement element, EntityEditorViewModel parent)
    {
        _parent = parent;
        _element = element;

        if (element is Levelable levelable)
        {
            LevelableData = new LevelableDataViewModel(levelable, _parent);
            if (element.AssociatedDefinition is LevelableDefinition levelableDefinition)
            {
                if (levelableDefinition.Variants != null && levelableDefinition.Variants.Count > 0)
                {
                    VariantList = new VariantListViewModel(levelable, this);
                }
            }
        }
        if (element is Character character)
        {
            CharacterData = new CharacterDataViewModel(character);
        }

        AllowedChildrenList = new AllowedChildrenViewModel(element.AssociatedDefinition.AllowedChildren);           
    }

    private readonly RPGElement _element;

    public RPGElement Element { get { return _element; } }

    private VariantListViewModel? _variantList = null;
    private AllowedChildrenViewModel _allowedChildrenList = null!;
    
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

    public void SetVariant(VariantDefinition variantDefinitionData)
    {
        if(_element is Levelable levelable)
        {
            levelable.Variant = variantDefinitionData;
            LevelableData?.RefreshProperties();
        }
    }
}