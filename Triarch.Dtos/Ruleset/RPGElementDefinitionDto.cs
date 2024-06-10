﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Triarch.Dtos.Ruleset;

public class RPGElementDefinitionDto
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string ElementName { get; set; } = null!;

    public int ElementTypeId { get; set; }

    [MaxLength(100)]
    public string? Description { get; set; } = null;  //Do we need a better name for this?  Used to reference description of effects

    [MaxLength(50)]
    public string? Stat { get; set; } = null;

    [MaxLength(25)]
    public string? PageNumbers { get; set; } = null;

    [DefaultValue(false)]
    public bool Human { get; set; } = false;

    //Needs Many-Many mapping:    
    public List<int> AllowedChildrenIds { get; set; } = [];

    public List<int> AllowedParentsIds { get; set; } = [];

    public LevelableDefinitionDto? LevelableData { get; set; } = null;

    public int? PointsContainerScale { get; set; } = null;
    
    public List<FreebieDto>? Freebies { get; set; } = null;    
}