using System.ComponentModel;
using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class TreeViewNodeNodeBESM3CAAttributeData
{
    public TreeViewNodeNodeBESM3CAAttributeDataAdditionalData AdditionalData { get ; set ; }

    public string Notes { get ; set ; }

    [XmlAttribute()]
    public string Name { get ; set ; }
    
    [XmlAttribute()]
    public int ID { get ; set ; }
}