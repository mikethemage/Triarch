using System.ComponentModel;

namespace Triarch.Dtos.Ruleset;

public class FreebieDto
{   
    public int Id { get; set; }
        
    public int FreebieElementDefinitionId { get; set; }    

    [DefaultValue(0)]
    public int FreeLevels { get; set; }  

    [DefaultValue(0)]
    public int RequiredLevels { get; set; }     
}