using System.ComponentModel.DataAnnotations.Schema;

namespace SmartReader.Core.Domain;

public class Extract
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Sql { get; set; }
    public string EndPoint { get; set; }
    public bool IsPriority { get; set; }
    public ICollection<ExtractHistory> Histories { get; set; } = new List<ExtractHistory>();

    [NotMapped] public string SqlCount => $"select count(1) from ({Sql})x";
}