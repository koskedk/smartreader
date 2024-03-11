using SmartReader.Contracts;

namespace SmartReader.Central.Application.Domain;

public class Pharmacy:IPharmacy
{
    public Guid Id { get; set; }
    public int PatientPK { get; set; }
    public int SiteCode { get; set; }
    public string? Drug { get; set; }
    public string? Provider { get; set; }
    public DateTime? DispenseDate { get; set; }
    public decimal? Duration { get; set; }
    public DateTime? ExpectedReturn { get; set; }
    public string? TreatmentType { get; set; }
    public string? RegimenLine { get; set; }
    public string? PeriodTaken { get; set; }
    public string? ProphylaxisType { get; set; }
}