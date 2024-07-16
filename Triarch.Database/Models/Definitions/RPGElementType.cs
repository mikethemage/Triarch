namespace Triarch.Database.Models.Definitions;

public partial class RPGElementType
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public int TypeOrder { get; set; }
    public bool BuiltIn { get; set; } = false;

    public int RPGSystemId { get; set; }

    public virtual RPGSystem RPGSystem { get; set; } = null!;
}
