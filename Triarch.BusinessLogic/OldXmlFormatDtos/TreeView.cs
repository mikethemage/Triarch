using System.Xml.Serialization;

namespace Triarch.BusinessLogic.OldXmlFormatDtos;

[Serializable()]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public class TreeView
{
    [XmlElement("node")]
    public TreeViewNode Node { get; set; } = null!;
}



















