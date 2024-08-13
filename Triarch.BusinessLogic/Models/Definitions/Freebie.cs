namespace Triarch.BusinessLogic.Models.Definitions;
public class Freebie
{
    public RPGElementDefinition FreebieElementDefinition { get; set; } = null!;

    public int FreeLevels { get; set; }

    public int RequiredLevels { get; set; }
}
