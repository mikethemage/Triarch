using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Triarch.Database.Models.Ruleset;

public class Freebie
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(FreebieElementDefinition))]
    public int FreebieElementDefinitionId { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public RPGElementDefinition FreebieElementDefinition { get; set; } = null!;

    [DefaultValue(0)]
    public int FreeLevels { get; set; }  //Denotes levels given for free i.e. 0 point cost

    [DefaultValue(0)]
    public int RequiredLevels { get; set; }  //Denotes required levels that must be paid for and cannot be removed

    [ForeignKey(nameof(OwnerElementDefinition))]
    [Required]
    public int OwnerElementDefinitionId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public RPGElementDefinition OwnerElementDefinition { get; set; } = null!;
}
