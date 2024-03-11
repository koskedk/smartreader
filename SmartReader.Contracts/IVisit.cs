namespace SmartReader.Contracts;

public interface IVisit:IPackage
{
    int PatientPK { get; set; }
    int SiteCode { get; set; }
    string? VisitBy { get; set; }
    string? Service { get; set; }
    string? VisitType { get; set; }
    int? WHOStage { get; set; }
    string? WABStage { get; set; }
    string? Pregnant { get; set; }
    DateTime? LMP { get; set; }
    DateTime? EDD { get; set; }
    decimal? Height { get; set; }
    decimal? Weight { get; set; }
}