using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[XmlType(AnonymousType = true)]
public class AttributeData
{
    public AttributeAdditionalData AdditionalData { get; set; } = null!;

    public string Notes { get; set; } = null!;

    [XmlAttribute()]
    public string Name { get; set; } = null!;

    [XmlAttribute()]
    public int ID { get; set; }
}