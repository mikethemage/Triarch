using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Triarch.Database.Models.Definitions;

public class RPGElementDefinition
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string ElementName { get; set; } = null!;

    public RPGElementType ElementType { get; set; } = null!;

    [MaxLength(100)]
    public string? Description { get; set; } = null;  //Do we need a better name for this?  Used to reference description of effects

    [MaxLength(50)]
    public string? Stat { get; set; } = null;

    [MaxLength(25)]
    public string? PageNumbers { get; set; } = null;

    [DefaultValue(false)]
    public bool Human { get; set; } = false;

    //Needs Many-Many mapping:    
    public ICollection<RPGElementDefinition> AllowedChildren { get; set; } = null!;

    public ICollection<RPGElementDefinition> AllowedParents { get; set; } = null!;

    public LevelableDefinition? LevelableData { get; set; } = null;

    public int? PointsContainerScale { get; set; } = null;

    [InverseProperty(nameof(Freebie.OwnerElementDefinition))]
    public ICollection<Freebie>? Freebies { get; set; } = null;

    
    public RPGSystem RPGSystem { get; set; } = null!;
}
