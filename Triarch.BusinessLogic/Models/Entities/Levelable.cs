using org.mariuszgromada.math.mxparser;
using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.BusinessLogic.Models.Entities;
public class Levelable : RPGElement
{
    public override string DisplayText
    {
        get
        {
            if (Variant != null)
            {
                return $"{Name} [{Variant.VariantName}] ({Points} Points)";
            }
            return base.DisplayText;
        }
    }

    public int MinLevel
    {
        get
        {
            if (AssociatedDefinition is LevelableDefinition levelableDefinition)
            {
                return levelableDefinition.MinLevel;
            }
            else
            {
                return 0;
            }
        }
    }

    public int MaxLevel
    {
        get
        {
            if (AssociatedDefinition is LevelableDefinition levelableDefinition)
            {
                return levelableDefinition.MaxLevel;
            }
            else
            {
                return int.MaxValue;
            }
        }
    }

    public int MaxEnforceableLevel
    {
        get
        {
            if (AssociatedDefinition is LevelableDefinition levelableDefinition
                && (levelableDefinition.EnforceMaxLevel == true
                || (levelableDefinition.Progression != null && levelableDefinition.Progression.Progressions.Count > 0)
                )
                )
            {
                return levelableDefinition.MaxLevel;
            }
            else
            {
                return int.MaxValue;
            }
        }
    }

    public int Level { get; set; } = 1;

    public int RequiredLevels { get; set; } = 0;

    public int FreeLevels { get; set; } = 0;

    public VariantDefinition? Variant { get; set; } = null;

    public override int BaseCost
    {
        get
        {
            return PointsPerLevel * (Level - FreeLevels);
        }
    }

    public virtual int PointsPerLevel
    {
        get
        {
            if (AssociatedDefinition is LevelableDefinition levelableDefinition)
            {
                if (levelableDefinition.Variants != null && Variant != null)
                {
                    return Variant.CostPerLevel;
                }
                else
                {
                    return levelableDefinition.CostPerLevel ?? 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }

    public override int Points
    {
        get
        {
            int points = (PointsPerLevel * Level) + VariablesOrRestrictions;
            if (AssociatedDefinition is LevelableDefinition levelableDefinition && levelableDefinition.Variants != null && Variant != null)
            {
                if(levelableDefinition.ElementName == "Weapon" && Variant.VariantName=="Alternate Attack")
                {
                    points /= 2;
                }                
            }
            return points;
        }
    }

    public string Description
    {
        get
        {
            string description = AssociatedDefinition.Description ?? "";

            if (Variant != null && Variant.Description != "")
            {
                description = Variant.Description;
            }

            if (AssociatedDefinition is LevelableDefinition levelableDefinition && levelableDefinition.Progression != null && levelableDefinition.Progression.CustomProgression)
            {
                ProgressionEntry? progressionEntry = levelableDefinition.Progression.Progressions.Where(x => x.ProgressionLevel == Level).FirstOrDefault();
                if (progressionEntry != null)
                {
                    return progressionEntry.Text;
                }
            }

            string completedDescription = "";

            while (description != "")
            {
                string[] pieces = description.Split('[', 2);
                completedDescription += pieces[0];
                if (pieces.Length > 1)
                {
                    description = pieces[1];
                    pieces = description.Split(']', 2);

                    completedDescription += ProcessDescriptionValue(pieces[0]);

                    if (pieces.Length > 1)
                    {
                        description = pieces[1];
                    }
                    else
                    {
                        description = "";
                    }
                }
                else
                {
                    description = "";
                }
            }

            return completedDescription;
        }
    }

    protected virtual string ProcessDescriptionValue(string valueToParse)
    {
        if (AssociatedDefinition is LevelableDefinition levelableDefinition)
        {
            if (levelableDefinition.Progression != null)
            {
                if (!levelableDefinition.Progression.Linear)
                {
                    string[] replacers = ["fn", "mn", "sn", "tn", "trn", "an", "rn", "tgn", "grn"];
                    foreach (var item in replacers)
                    {
                        if (int.TryParse(valueToParse.Replace(item, ""), out int i))
                        {
                            int progressionLevel;
                            if (levelableDefinition.ProgressionReversed ?? false)
                            {
                                progressionLevel = i - (Level - 1);
                            }
                            else
                            {
                                progressionLevel = i - 1 + Level;
                            }
                            ProgressionEntry? progressionEntry = levelableDefinition.Progression.Progressions.Where(x => x.ProgressionLevel == progressionLevel).FirstOrDefault();
                            if (progressionEntry != null)
                            {
                                return progressionEntry.Text;
                            }
                            else
                            {
                                return "MISSING PROGRESSION";
                            }
                        }
                    }
                }

                valueToParse = valueToParse.Replace("n", Level.ToString());
                Expression e = new Expression(valueToParse);

                return e.calculate().ToString();
            }
        }
        return valueToParse;
    }

    public override int HealthAdj
    {
        get
        {
            if (!Children.Any(x => x.AssociatedDefinition.ElementType.TypeName == "Restriction") && AssociatedDefinition.ElementName == "Tough")
            {
                return Level * 5;
            }
            return base.HealthAdj;
        }
    }

    public override int EnergyAdj
    {
        get
        {
            if (!Children.Any(x => x.AssociatedDefinition.ElementType.TypeName == "Restriction") && AssociatedDefinition.ElementName == "Energy Bonus")
            {
                return Level * 5;
            }
            return base.HealthAdj;
        }
    }

    public override int ACVAdj
    {
        get
        {
            if (!Children.Any(x => x.AssociatedDefinition.ElementType.TypeName == "Restriction") && AssociatedDefinition.ElementName == "Attack Combat Mastery")
            {
                return Level;
            }
            return base.HealthAdj;
        }
    }

    public override int DCVAdj
    {
        get
        {
            if (!Children.Any(x => x.AssociatedDefinition.ElementType.TypeName == "Restriction") && AssociatedDefinition.ElementName == "Defence Combat Mastery")
            {
                return Level;
            }
            return base.HealthAdj;
        }
    }
}
