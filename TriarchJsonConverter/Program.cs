using System.Text.Json;
using System.Text.Json.Serialization;
using Triarch.Dtos.Definitions;
using TriarchJsonConverter.Serialization;

namespace TriarchJsonConverter;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Converting...");

        string inputText = File.ReadAllText("DataFiles\\BESM3E.json");

        MasterListingSerialized? inputData = JsonSerializer.Deserialize<MasterListingSerialized>(inputText);

        if (inputData != null)
        {
            Console.WriteLine("Read data successfully!");

            CoreRulesetDto outputRuleset = new CoreRulesetDto { CoreRulesetName = inputData.ListingName };

            RPGSystemDto outputSystem = new RPGSystemDto
            {
                Ruleset = outputRuleset,
                SystemName = "BESM 3rd Edition",
                DescriptiveName = "Core Ruleset for BESM 3rd Edition",
                OwnerUserId = 1,
                ElementDefinitions = new List<RPGElementDefinitionDto>(),
                ElementTypes = new List<RPGElementTypeDto>(),
                Genres = new List<GenreDto>(),
                Progressions = new List<ProgressionDto>()
            };
            PopulateTypeList(inputData, outputSystem);
            PopulateGenreList(inputData, outputSystem);
            PopulateProgressionList(inputData, outputSystem);
            PopulateAttributes(inputData, outputSystem);
            BuildParentLists(outputSystem);

            Console.WriteLine("Conversion Complete");

            WriteOutOutputData(outputSystem);

            Console.WriteLine("Written data successfully!");
        }

    }

    private static void BuildParentLists(RPGSystemDto outputSystem)
    {
        foreach (RPGElementDefinitionDto parent in outputSystem.ElementDefinitions.Where(x => x.AllowedChildrenNames.Count > 0))
        {
            foreach (string childName in parent.AllowedChildrenNames)
            {
                RPGElementDefinitionDto? child = outputSystem.ElementDefinitions.Where(x => x.ElementName == childName).FirstOrDefault();
                if (child != null)
                {
                    if (child.AllowedParentsNames == null)
                    {
                        child.AllowedParentsNames = new List<string>();
                    }
                    child.AllowedParentsNames.Add(parent.ElementName);
                }
            }
        }
    }

    private static void WriteOutOutputData(RPGSystemDto outputSystem)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        string outputText = JsonSerializer.Serialize(outputSystem, options);
        File.WriteAllText("DataFiles\\NEW_BESM3E.json", outputText);
    }

    private static void PopulateAttributes(MasterListingSerialized inputData, RPGSystemDto outputSystem)
    {
        foreach (DataListingSerialized inputElement in inputData.AttributeList)
        {
            RPGElementDefinitionDto outputElement = new RPGElementDefinitionDto
            {
                ElementName = inputElement.Name,
                ElementTypeName = inputElement.Type,
                Description = inputElement.Description,
                Human = inputElement.Human,
                PageNumbers = inputElement.Page,
                Stat = inputElement.Stat,
                PointsContainerScale = inputElement.PointScale
            };

            if (inputElement.ChildrenList != null && inputElement.ChildrenList != "")
            {
                IEnumerable<int> childIds = inputElement.ChildrenList.Split(',').Select(x => int.Parse(x));
                foreach (int childId in childIds)
                {
                    DataListingSerialized? childAttribute = inputData.AttributeList.Where(x => x.ID == childId).FirstOrDefault();
                    if (childAttribute != null)
                    {
                        if (outputElement.AllowedChildrenNames == null)
                        {
                            outputElement.AllowedChildrenNames = new List<string>();
                        }
                        outputElement.AllowedChildrenNames.Add(childAttribute.Name);
                    }
                }
            }

            if (inputElement.HasLevel)
            {
                LevelableDefinitionDto levellableData = new LevelableDefinitionDto
                {
                    MaxLevel = inputElement.MaxLevel,
                    CostPerLevel = inputElement.CostperLevel,
                    CostPerLevelDescription = inputElement.CostperLevelDesc,

                    EnforceMaxLevel = inputElement.EnforceMaxLevel,
                    SpecialPointsPerLevel = inputElement.SpecialPointsPerLevel
                };

                if (inputElement.Progression != null
                    && inputElement.Progression != "")
                {

                    if(outputSystem.Progressions.Any(x=>x.ProgressionType==inputElement.Progression))
                    {
                        bool reversed = false;
                        string progressionName = inputElement.Progression;

                        if (progressionName.EndsWith(" Rev"))
                        {
                            reversed = true;
                            progressionName = progressionName.Replace(" Rev", "");
                        }

                        levellableData.ProgressionName = progressionName;
                        levellableData.ProgressionReversed = reversed;
                    }                    
                }
                else if (inputElement.CustomProgression != null && inputElement.CustomProgression.Count > 0)
                {
                    string customProgressionName = inputElement.Name + "Custom";
                    levellableData.ProgressionName = customProgressionName;

                    ProgressionDto customProgression = new ProgressionDto
                    {
                        ProgressionType = customProgressionName,
                        Progressions = new List<ProgressionEntryDto>(),
                        CustomProgression = true
                    };

                    int j = 1; //Start at level 1 for custom progressions
                    foreach (string progressionEntry in inputElement.CustomProgression)
                    {
                        customProgression.Progressions.Add(new ProgressionEntryDto
                        {
                            ProgressionLevel = j,
                            Text = progressionEntry
                        });
                        j++;
                    }

                    outputSystem.Progressions.Add(customProgression);
                }

                if (inputElement.Variants != null && inputElement.Variants.Count > 0)
                {
                    levellableData.Variants = new List<VariantDefinitionDto>();

                    foreach (VariantListingSerialized inputVariant in inputElement.Variants)
                    {
                        VariantDefinitionDto outputVariant = new VariantDefinitionDto
                        {
                            VariantName = inputVariant.Name,
                            CostPerLevel = inputVariant.CostperLevel,
                            Description = inputVariant.Desc,
                            IsDefault = inputVariant.DefaultVariant
                        };
                        levellableData.Variants.Add(outputVariant);
                    }
                }

                if (inputElement.MultiGenre == true && inputElement.GenrePoints!=null)
                {
                    levellableData.MultiGenreCostPerLevels = new List<GenreCostPerLevelDto>();
                    for (int i = 0; i < inputElement.GenrePoints.Count; i++)
                    {
                        GenreDto? genre = outputSystem.Genres.Where(x => x.GenreOrder == i).FirstOrDefault();
                        if (genre != null)
                        {
                            levellableData.MultiGenreCostPerLevels.Add(new GenreCostPerLevelDto
                            {
                                GenreName = genre.GenreName,
                                CostPerLevel = inputElement.GenrePoints[i]
                            });
                        }
                    }
                }

                outputElement.LevelableData = levellableData;
            }

            if (inputElement.HasFreebie)
            {
                DataListingSerialized? freebieAttribute = inputData.AttributeList.Where(x => x.ID == inputElement.SubAttributeID).FirstOrDefault();
                if (freebieAttribute != null)
                {
                    int freeLevels = 0;
                    if (freebieAttribute.CostperLevel != 0)
                    {
                        freeLevels = (int)inputElement.SubAttributePointsAdj! / freebieAttribute.CostperLevel;
                    }

                    FreebieDto outputFreebie = new FreebieDto
                    {
                        FreeLevels = freeLevels,
                        RequiredLevels = (int)inputElement.SubAttributeLevel! - freeLevels,
                        FreebieElementDefinitionName = freebieAttribute.Name
                    };

                    outputElement.Freebies = new List<FreebieDto>();
                    outputElement.Freebies.Add(outputFreebie);
                }
            }

            outputSystem.ElementDefinitions.Add(outputElement);
        }
    }

    private static void PopulateProgressionList(MasterListingSerialized inputData, RPGSystemDto outputSystem)
    {
        foreach (ProgressionListingSerialized progressionListing in inputData.ProgressionList)
        {
            ProgressionDto progressionDto = new ProgressionDto
            {
                ProgressionType = progressionListing.ProgressionType,
                Progressions = new List<ProgressionEntryDto>(),
                CustomProgression = false
            };            

            int j = 0;
            foreach (string progressionEntry in progressionListing.ProgressionsList)
            {
                progressionDto.Progressions.Add(new ProgressionEntryDto
                {
                    ProgressionLevel = j,
                    Text = progressionEntry
                });
                j++;
            }

            outputSystem.Progressions.Add(progressionDto);
        }

        outputSystem.Progressions.Add(new ProgressionDto
        {
            ProgressionType = "Linear",
            CustomProgression = false,
            Linear = true
        });
    }

    private static void PopulateGenreList(MasterListingSerialized inputData, RPGSystemDto outputSystem)
    {
        int i = 0;
        foreach (string genreName in inputData.Genres)
        {
            outputSystem.Genres.Add(
                new GenreDto
                {
                    GenreName = genreName,
                    GenreOrder = i
                }
            );
            i++;
        }
    }

    private static void PopulateTypeList(MasterListingSerialized inputData, RPGSystemDto outputSystem)
    {
        foreach (TypeListingSerialized typeListing in inputData.TypeList)
        {
            outputSystem.ElementTypes.Add(
                new RPGElementTypeDto
                {
                    TypeName = typeListing.Name,
                    TypeOrder = typeListing.TypeOrder
                }
            );
        }
    }
}
