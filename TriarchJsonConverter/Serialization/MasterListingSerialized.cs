using System.Text.Json;
using System.Text.Json.Serialization;

namespace TriarchJsonConverter.Serialization;

public class MasterListingSerialized
{
    public List<DataListingSerialized> AttributeList { get; set; } = null!;
    public List<TypeListingSerialized> TypeList { get; set; } = null!;
    public string ListingName { get; set; } = null!;
    public List<string> Genres { get; set; } = null!;
    public List<ProgressionListingSerialized> ProgressionList { get; set; } = null!;

    private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true };

    public void CreateJSON(string outputPath)
    {
        //Code to write out JSON data files.   
        //Should not be being called at present - debugging only:
        string output = JsonSerializer.Serialize<MasterListingSerialized>(this, _serializerOptions);

        File.WriteAllText(outputPath, output);

    }
}