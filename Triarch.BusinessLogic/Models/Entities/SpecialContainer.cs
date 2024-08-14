using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.BusinessLogic.Models.Entities;
public class SpecialContainer : Levelable
{
    public override string DisplayText
    {
        get
        {
            string mainText;
            if (Variant != null)
            {
                mainText = $"{Name} [{Variant.VariantName}] ({Points})";
            }
            else
            {
                mainText = base.DisplayText;
            }
            return $"{mainText} [{SpecialPointsRemaining} Remaining]";
        }
    }

    public int SpecialPointsTotal
    {
        get
        {
            if (AssociatedDefinition is SpecialContainerDefinition specialContainerDefinition)
            {
                return Level * specialContainerDefinition.SpecialPointsPerLevel;
            }
            return 0;
        }
    }

    public int SpecialPointsUsed
    {
        get
        {
            return ChildPoints;
        }
    }



    public int SpecialPointsRemaining
    {
        get
        {
            return SpecialPointsTotal - SpecialPointsUsed;
        }
    }

}
