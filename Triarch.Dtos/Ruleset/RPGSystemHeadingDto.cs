﻿namespace Triarch.Dtos.Ruleset;

public class RPGSystemHeadingDto
{
    public int Id { get; set; }
    public string SystemName { get; set; } = null!;

    public string? CoreRulesetName { get; set; } = null;
}