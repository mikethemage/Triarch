namespace TriarchDbFirst;

public partial class RPGElementType
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public int TypeOrder { get; set; }

    public int RPGSystemId { get; set; }

    public virtual RPGSystem RPGSystem { get; set; } = null!;
}
