using System.ComponentModel;
using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class TreeViewNodeBESM3CACharacterDataAdditionalDataCharacterStats
{
    [XmlAttribute()]
    public byte Mind
    {
        get
        ;
        set
        ;
    }

    [XmlAttribute()]
    public byte Body
    {
        get
        ;
        set
        ;
    }

    [XmlAttribute()]
    public byte Soul
    {
        get
        ;
        set
        ;
    }
}