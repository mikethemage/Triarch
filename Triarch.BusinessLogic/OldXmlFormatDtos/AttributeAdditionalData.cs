using System.ComponentModel;
using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class TreeViewNodeNodeBESM3CAAttributeDataAdditionalData
{
    public TreeViewNodeNodeBESM3CAAttributeDataAdditionalDataAttributeStats AttributeStats { get; set; }

    [XmlAttribute()]
    public string Type { get; set; }
}