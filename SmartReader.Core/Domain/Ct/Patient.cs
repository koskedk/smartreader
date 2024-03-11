using SmartReader.Contracts;

namespace SmartReader.Core.Domain.Ct;

public class Patient:IPatient
{
    public Guid Id { get; set; }
    public int PatientPK { get; set; }
    public int SiteCode { get; set; }
    public string? PatientID { get; set; }
    public string? NUPI { get; set; }
    public string? FacilityName { get; set; }
    public string? Gender { get; set; }
    public DateTime? DOB { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public string? Pkv { get; set; }
    public string? Occupation { get; set; }
}