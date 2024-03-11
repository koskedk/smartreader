namespace SmartReader.Core.Domain;

public class Config
{
    public string Id { get; set; }
    public ConfigType Type { get; set; }
    public string Value { get; set; }
    public string? Description { get; set; }

    public int GetNumericValue()
    {
        return Convert.ToInt32(Value);
    }
}