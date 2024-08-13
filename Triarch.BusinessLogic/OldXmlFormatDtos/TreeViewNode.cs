using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[XmlType(AnonymousType = true)]
public class TreeViewNode
{
    [XmlElement("BESM3CA.CharacterData")]
    public CharacterData? BESM3CACharacterData { get; set; }

    [XmlElement("BESM3CA.AttributeData")]
    public AttributeData? BESM3CAAttributeData { get; set; }

    [XmlElement("node")]
    public TreeViewNode[]? Node { get; set; }

    [XmlAttribute("text")]
    public string Text { get; set; } = null!;
}