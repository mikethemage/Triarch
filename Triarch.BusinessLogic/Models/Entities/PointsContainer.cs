using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.BusinessLogic.Models.Entities;
public class PointsContainer : RPGElement
{
    public override int Points
    {
        get
        {
            int tempPoints = VariablesOrRestrictions;

            if (AssociatedDefinition is PointsContainerDefinition pointsContainerDefinition)
            {
                if (pointsContainerDefinition.PointsContainerScale != 0)
                {
                    //"[...]divide by two (round down; minimum of 0 Points). [...] Note that this makes Items that
                    //are described with only one Feature Attribute(such as a
                    //camera) effectively free."

                    if (ChildPoints > 0 )
                    {
                        //If child points positive, scale points e.g. 3E items valued at 1/2 total child points:
                        tempPoints += ChildPoints / pointsContainerDefinition.PointsContainerScale;
                    }
                }
            }

            return tempPoints;
        }
    }
}
