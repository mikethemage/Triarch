using System.ComponentModel;
using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class TreeViewNodeBESM3CACharacterDataAdditionalData
{
    public TreeViewNodeBESM3CACharacterDataAdditionalDataCharacterStats CharacterStats { get; set; }

    [XmlAttribute()]
    public string Type { get ; set ; }
}