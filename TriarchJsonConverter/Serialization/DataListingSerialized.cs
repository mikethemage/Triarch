﻿using System.Collections.Generic;

namespace TriarchJsonConverter.Serialization;

public class DataListingSerialized
{
    //Properties:
    public int ID { get; set; }
    public string Name { get; set; }
    public string CostperLevelDesc { get; set; }
    public string Progression { get; set; }
    public int MaxLevel { get; set; }
    public string Stat { get; set; }
    public string Page { get; set; }
    public bool Human { get; set; }
    public string Type { get; set; }
    public bool Container { get; set; }
    public int CostperLevel { get; set; }
    public bool RequiresVariant { get; set; }
    public int? SpecialPointsPerLevel { get; set; }
    public bool SpecialContainer { get; set; }
    public bool EnforceMaxLevel { get; set; }
    public string Description { get; set; }
    public List<string> CustomProgression { get; set; }
    public List<int> GenrePoints { get; set; }
    public string ChildrenList { get; set; }
    public List<VariantListingSerialized> Variants { get; set; }
    public bool HasLevel { get; set; }
    public bool MultiGenre { get; set; }
    public bool PointsContainer { get; set; }
    public int? PointScale { get; set; }
    public bool Companion { get; set; }
    public bool HasFreebie { get; set; }
    public int? SubAttributeID { get; set; }
    public int? SubAttributeLevel { get; set; }
    public int? SubAttributePointsAdj { get; set; }
}
