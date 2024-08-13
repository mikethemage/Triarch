using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[XmlType(AnonymousType = true)]
public class CharacterAdditionalData
{
    public CharacterStats CharacterStats { get; set; } = null!;

    [XmlAttribute()]
    public string Type { get; set; } = null!;
}