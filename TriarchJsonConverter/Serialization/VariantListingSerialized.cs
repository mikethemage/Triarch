namespace TriarchJsonConverter.Serialization;

public class VariantListingSerialized
{
    public int ID { get; set; }
    public string Name { get; set; } = null!;
    public int CostperLevel { get; set; }
    public string? Desc { get; set; }
    public bool DefaultVariant { get; set; }
}

