﻿using System.ComponentModel.DataAnnotations;

namespace Triarch.Dtos.Ruleset;

public class ProgressionEntryDto
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Text { get; set; } = string.Empty;

    public int ProgressionLevel { get; set; }
}