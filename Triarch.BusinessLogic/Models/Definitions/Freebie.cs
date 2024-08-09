using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triarch.BusinessLogic.Models.Definitions;
public class Freebie
{    
    public RPGElementDefinition FreebieElementDefinition { get; set; } = null!;
    
    public int FreeLevels { get; set; }
    
    public int RequiredLevels { get; set; }
}
