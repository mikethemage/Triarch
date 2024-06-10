﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Triarch.Dtos.Definitions;

public class ProgressionDto
{   
    public int Id { get; set; }

    [MaxLength(100)]
    public string ProgressionType { get; set; } = string.Empty;

    [DefaultValue(false)]
    public bool CustomProgression { get; set; } = false;

    public List<ProgressionEntryDto> Progressions { get; set; } = [];
}