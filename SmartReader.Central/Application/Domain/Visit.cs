using SmartReader.Contracts;

namespace SmartReader.Central.Application.Domain;

public class Visit:IVisit
{
    public Guid Id { get; set; }
    public int PatientPK { get; set; }
    public int SiteCode { get; set; }
    public string? VisitBy { get; set; }
    public string? Service { get; set; }
    public string? VisitType { get; set; }
    public int? WHOStage { get; set; }
    public string? WABStage { get; set; }
    public string? Pregnant { get; set; }
    public DateTime? LMP { get; set; }
    public DateTime? EDD { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
}