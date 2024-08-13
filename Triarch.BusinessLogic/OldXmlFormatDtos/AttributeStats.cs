using System.Xml;
using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[XmlType(AnonymousType = true)]
public class AttributeStats
{
    [XmlAttribute()]
    public int Level { get; set; }

    [XmlAttribute()]
    public int Variant { get; set; }

    [XmlAttribute()]
    public string HasLevel { get; set; } = null!;

    [XmlAttribute()]
    public int Points { get; set; }
}