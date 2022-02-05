using System.Text.Json;

namespace PomodoroCLI;

public class JsonWriter : IFileWriter
{
    private string FileLocation { get; set; }

    public JsonWriter(string fileLocation)
    {
        FileLocation = fileLocation;
    }
    
    public void WriteToFile<T>(T value)
    {
        var configJson = File.ReadAllText(FileLocation);
        var key = value?.GetType().ToString().Split('.').Last();
        var config = JsonSerializer.Deserialize<Dictionary<string, object?>>(configJson);
        if (config == null)
        {
            throw new InvalidOperationException("File either does not exist or is not a JSON file");
        }

        if (key != null) config[key] = value;

        var updatedConfig = JsonSerializer.Serialize(config, new JsonSerializerOptions {WriteIndented = true});

        File.WriteAllText(FileLocation, updatedConfig);
    }
}