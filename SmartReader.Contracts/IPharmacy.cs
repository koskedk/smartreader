namespace SmartReader.Contracts;

public interface IPharmacy:IPackage
{
    int PatientPK { get; set; }
    int SiteCode { get; set; }
    string? Drug { get; set; }
    string? Provider { get; set; }
    DateTime? DispenseDate { get; set; }
    decimal? Duration { get; set; }
    DateTime? ExpectedReturn { get; set; }
    string? TreatmentType { get; set; }
    string? RegimenLine { get; set; }
    string? PeriodTaken { get; set; }
    string? ProphylaxisType { get; set; }
}