using System.ComponentModel;
using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class TreeViewNodeBESM3CACharacterData
{
    public TreeViewNodeBESM3CACharacterDataAdditionalData AdditionalData
    {
        get
        ;
        set
        ;
    }

    /// <remarks/>
    public string Notes
    {
        get
        ;
        set
        ;
    }

    /// <remarks/>
    [XmlAttribute()]
    public string Name
    {
        get
        ;
        set
        ;
    }

    /// <remarks/>
    [XmlAttribute()]
    public int ID
    {
        get
        ;
        set
        ;
    }
}