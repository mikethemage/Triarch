using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triarch.RPGSystem.Models;

public class VariantDefinition
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string VariantName { get; set; } = null!;

    public int CostPerLevel { get; set; }

    [MaxLength(250)]
    public string? Description { get; set; } = null;

    [DefaultValue(false)]
    public bool IsDefault {  get; set; } = false;    

    public RPGElementDefinition ElementDefinition { get; set; } = null!;
}
