using System.ComponentModel;
using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class TreeViewNode
{
    [XmlElement("BESM3CA.CharacterData")]
    public TreeViewNodeBESM3CACharacterData? BESM3CACharacterData { get; set; }

    [XmlElement("BESM3CA.AttributeData")]
    public TreeViewNodeNodeBESM3CAAttributeData? BESM3CAAttributeData { get; set; }

    [XmlElement("node")]
    public TreeViewNode[]? node { get; set; }

    [XmlAttribute()]
    public string text { get; set; }
}