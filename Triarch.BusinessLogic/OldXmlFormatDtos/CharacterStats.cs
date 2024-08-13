using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[XmlType(AnonymousType = true)]
public class CharacterStats
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