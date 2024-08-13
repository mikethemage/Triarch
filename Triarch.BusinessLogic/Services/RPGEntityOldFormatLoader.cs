using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;
using Triarch.BusinessLogic.OldXmlFormatDtos;

namespace Triarch.BusinessLogic.Services;

public class RPGEntityOldFormatLoader
{
    public RPGEntity? Load(string xmlText, RPGSystem rPGSystem)
    {


        XmlSerializer xmlSerializer = new XmlSerializer(typeof(TreeView));

        using StringReader reader = new StringReader(xmlText);
        TreeView? treeView = (TreeView?)xmlSerializer.Deserialize(reader);
        if (treeView == null)
        {
            throw new Exception("Failed to parse XML");
        }

        RPGEntity output = new RPGEntity
        {
            EntityName = treeView.node.BESM3CACharacterData?.Name ?? "",
            EntityType = "Character",
            Genre = rPGSystem.Genres[0],
            RPGSystem = rPGSystem
        };

        output.RootElement = ConvertToElement(treeView.node, rPGSystem, output);

        return output;
    }

    private RPGElement ConvertToElement(TreeViewNode node, RPGSystem rPGSystem, RPGEntity owner)
    {
        if (node.BESM3CACharacterData != null)
        {
            var characterDefinition = rPGSystem.ElementDefinitions.Where(x=>x.ElementName=="Character").FirstOrDefault();
            if (characterDefinition != null)
            {
                RPGElement element = characterDefinition.CreateNode(owner, node.BESM3CACharacterData.Notes, false);
                if (element is Character character)
                {
                    character.Body = node.BESM3CACharacterData.AdditionalData.CharacterStats.Body;
                    character.Mind = node.BESM3CACharacterData.AdditionalData.CharacterStats.Mind;
                    character.Soul = node.BESM3CACharacterData.AdditionalData.CharacterStats.Soul;
                }

                if (node.node is not null)
                {
                    foreach (TreeViewNode child in node.node)
                    {
                        RPGElement childElement = ConvertToElement(child, rPGSystem, owner);
                        element.Children.Add(childElement);
                        childElement.Parent = element;
                    }
                }                    

                return element;
            }
        }

        if (node.BESM3CAAttributeData != null)
        {
            var temp = node.BESM3CAAttributeData.Name.Split(" [");
            string elementName=temp[0];
            //Fix for spelling error in original version:
            if(elementName== "Somantic Components")
            {
                elementName = "Somatic Components";
            }
            string? variantName = null;
            if(temp.Length > 1)
            {
                variantName = temp[1].Replace("]","");
            }
            var elementDefinition = rPGSystem.ElementDefinitions.Where(x=>x.ElementName==elementName).FirstOrDefault();
            if (elementDefinition != null)
            {
                RPGElement element;
                if (elementDefinition is LevelableDefinition levelableDefinition)
                {
                    element = levelableDefinition.CreateNode(owner, node.BESM3CAAttributeData.Notes, node.BESM3CAAttributeData.AdditionalData.AttributeStats.Level, false);

                    if (variantName != null) 
                    {
                        ((Levelable)element).Variant = levelableDefinition.Variants?.Where(x=>x.VariantName==variantName).FirstOrDefault();
                    }                    
                }
                else
                {
                    element = elementDefinition.CreateNode(owner, node.BESM3CAAttributeData.Notes, false);
                }

                if (node.node is not null)
                {
                    foreach (TreeViewNode child in node.node)
                    {
                        RPGElement childElement = ConvertToElement(child, rPGSystem, owner);
                        element.Children.Add(childElement);
                        childElement.Parent = element;
                    }
                }

                

                return element;                
            }
        }

        throw new Exception("Unable to convert node!");
    }
}
