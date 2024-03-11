namespace SmartReader.Core.Domain;

public class ExtractHistory
{
    public int Id  { get; set; }
    public long Loaded { get; set; }
    public long? Sent { get; set; }
    public DateTime? Date { get; set; }
    public string? Status { get; set; }
    public int ExtractId { get; set; }
    
    public ExtractHistory()
    {
    }

    public ExtractHistory(long loaded, int extractId)
    {
        Loaded = loaded;
        ExtractId = extractId;
        Date=DateTime.Now;
    }
}