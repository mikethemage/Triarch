﻿namespace Triarch.BusinessLogic.Models.Definitions;

public class RPGElementType
{
    public string TypeName { get; set; } = null!;

    public bool BuiltIn { get; set; } = false;

    public int TypeOrder { get; set; }
}
