using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.BusinessLogic.Services;
public class TextExporter
{
    public string Export(RPGEntity entity)
    {
        List<string> exportComponents = new List<string>();
        int indentLevel = 0;
        exportComponents.Add($"Triarch {entity.EntityType} Export");
        exportComponents.Add($"Template: {entity.RPGSystem.SystemName}");
        exportComponents.Add("");

        AddElement(entity.RootElement, exportComponents, indentLevel);

        return string.Join('\n', exportComponents);
    }

    private void AddElement(RPGElement element, List<string> exportComponents, int indentLevel)
    {
        var indent = new string('\t', indentLevel);
        exportComponents.Add($"{indent}{element.Name} ({element.Points} Points)");

        if (element is PointsContainer container)
        {
            exportComponents.Add($"{indent}(");
        }

        indentLevel++;
        indent = new string('\t', indentLevel);

        if (element is Character character)
        {
            exportComponents.Add($"{indent}Mind: {character.Mind}");
            exportComponents.Add($"{indent}Body: {character.Body}");
            exportComponents.Add($"{indent}Soul: {character.Soul}");            
            exportComponents.Add($"{indent}ACV: {character.ACV}");
            exportComponents.Add($"{indent}DCV: {character.DCV}");
            exportComponents.Add($"{indent}Health: {character.Health}");
            exportComponents.Add($"{indent}Energy: {character.Energy}");                  
        }
        if (element is Levelable levelable)
        {
            exportComponents.Add($"{indent}Level {levelable.Level} x {levelable.PointsPerLevel} = {levelable.BaseCost}");
            exportComponents.Add($"{indent}Description: {levelable.Description}");
        }
        
        if(!string.IsNullOrWhiteSpace(element.Notes))
        {
            exportComponents.Add($"{indent}[Notes: {element.Notes}]");
        }
        
        exportComponents.Add("");
        foreach (RPGElement child in element.Children)
        {
            AddElement(child, exportComponents, indentLevel);
        }

        if (element is PointsContainer)
        {
            indentLevel--;
            indent = new string('\t', indentLevel);
            exportComponents.Add($"{indent}) / {((PointsContainerDefinition)element.AssociatedDefinition).PointsContainerScale}");
        }
    }
}
