﻿using System.ComponentModel.DataAnnotations;

namespace Triarch.RPGSystem.Models;

public class ProgressionEntry
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Text { get; set; } = null!;

    public int ProgressionLevel { get; set; }
    
    public Progression Progression { get; set; } = null!;
}
