using System.Xml.Serialization;
using System.Xml;
using System.ComponentModel;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class TreeViewNodeNodeBESM3CAAttributeDataAdditionalDataAttributeStats
{
    [XmlAttribute()]
    public int Level { get ; set ; }
    
    [XmlAttribute()]
    public int Variant { get ; set ; }
    
    [XmlAttribute()]
    public string HasLevel { get ; set ; }

    [XmlAttribute()]
    public int Points { get;  set; }
}