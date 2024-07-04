namespace Triarch.Database.Models.Definitions;

public partial class CoreRuleset
{
    public int Id { get; set; }

    public string CoreRulesetName { get; set; } = string.Empty;

    public virtual ICollection<RPGSystem> RPGSystems { get; set; } = new List<RPGSystem>();
}
