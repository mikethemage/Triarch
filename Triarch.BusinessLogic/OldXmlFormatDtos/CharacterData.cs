using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[XmlType(AnonymousType = true)]
public class CharacterData
{
    public CharacterAdditionalData AdditionalData { get; set; } = null!;

    /// <remarks/>
    public string Notes { get; set; } = null!;

    /// <remarks/>
    [XmlAttribute()]
    public string Name { get; set; } = null!;

    /// <remarks/>
    [XmlAttribute()]
    public int ID { get; set; }
}