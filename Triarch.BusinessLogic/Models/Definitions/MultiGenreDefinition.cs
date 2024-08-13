﻿using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.BusinessLogic.Models.Definitions;
public class MultiGenreDefinition : LevelableDefinition
{
    public List<GenreCostPerLevel> MultiGenreCostPerLevels { get; set; } = new();

    public override RPGElement CreateNode(RPGEntity ownerEntity, string notes, int level, bool isFreebie = false, int freeLevels = 0, int requiredLevels = 0)
    {
        VariantDefinition? defaultVariant = null;

        if (Variants != null)
        {
            defaultVariant = Variants.Where(x => x.IsDefault).FirstOrDefault();
        }

        return new MultiGenre
        {
            AssociatedDefinition = this,
            Entity = ownerEntity,
            Notes = notes,
            IsFreebie = isFreebie,
            Level = level,
            FreeLevels = freeLevels,
            RequiredLevels = requiredLevels,
            Variant = defaultVariant
        };

    }


}
