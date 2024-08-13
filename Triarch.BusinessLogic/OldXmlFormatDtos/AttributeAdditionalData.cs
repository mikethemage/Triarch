using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[XmlType(AnonymousType = true)]
public class AttributeAdditionalData
{
    public AttributeStats AttributeStats { get; set; } = null!;

    [XmlAttribute()]
    public string Type { get; set; } = null!;
}