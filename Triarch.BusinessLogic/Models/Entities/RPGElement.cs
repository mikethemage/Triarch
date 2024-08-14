using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.BusinessLogic.Models.Entities;
public abstract class RPGElement
{
    public RPGEntity Entity { get; set; } = null!;
    public required virtual RPGElementDefinition AssociatedDefinition { get; init; }
    public virtual string Name { get { return AssociatedDefinition.ElementName; } }
    public bool IsFreebie { get; set; } = false;
    public virtual int BaseCost { get { return 0; } }
    public virtual int Points { get; protected set; } = 0;

    public virtual string DisplayText
    {
        get
        {
            return $"{Name} ({Points})";
        }
    }

    public string Notes { get; set; } = string.Empty;
    public RPGElement? Parent { get; set; } = null;
    public List<RPGElement> Children { get; init; } = new List<RPGElement>();
    public List<RPGElementDefinition> PotentialChildren
    {
        get
        {
            return AssociatedDefinition.AllowedChildren.Where(x => x.ElementType.BuiltIn == false).ToList();
        }
    }

    public virtual int HealthAdj { get { return 0; } }
    public virtual int EnergyAdj { get { return 0; } }
    public virtual int ACVAdj { get { return 0; } }
    public virtual int DCVAdj { get { return 0; } }

    public bool CanMoveUp()
    {
        if (Parent == null)
        {
            return false;
        }

        if (Parent.Children.First() == this)
        {
            return false;
        }

        return true;
    }

    public bool CanMoveDown()
    {
        if (Parent == null)
        {
            return false;
        }

        if (Parent.Children.Last() == this)
        {
            return false;
        }

        return true;
    }

    public bool CanDelete()
    {
        if (Parent == null)
        {
            return false;
        }
        if (IsFreebie)
        {
            return false;
        }
        return true;
    }

    public void AddFreebies()
    {
        foreach (Freebie freebie in AssociatedDefinition.Freebies)
        {
            RPGElement freebieToAdd;
            if (freebie.FreebieElementDefinition is LevelableDefinition levelableFreebie)
            {
                freebieToAdd = levelableFreebie.CreateNode(Entity, "", freebie.FreeLevels + freebie.RequiredLevels, true, freebie.FreeLevels, freebie.RequiredLevels);

            }
            else
            {
                freebieToAdd = freebie.FreebieElementDefinition.CreateNode(Entity, "", true);
            }
            Children.Add(freebieToAdd);
            freebieToAdd.Parent = this;
        }
    }

    public int ChildPoints
    {
        get
        {
            int childPoints = 0;

            foreach (RPGElement child in Children)
            {
                if (child.AssociatedDefinition.ElementType.TypeName != "Restriction" && child.AssociatedDefinition.ElementType.TypeName != "Variable")
                {
                    childPoints += child.Points;
                }
            }

            return childPoints;
        }
    }

    public int VariablesOrRestrictions
    {
        get
        {
            int variablesOrRestrictions = 0;
            foreach (RPGElement child in Children)
            {
                if (child.AssociatedDefinition.ElementType.TypeName == "Restriction" || child.AssociatedDefinition.ElementType.TypeName == "Variable")
                {
                    variablesOrRestrictions += child.Points;
                }
            }
            return variablesOrRestrictions;
        }
    }
}
