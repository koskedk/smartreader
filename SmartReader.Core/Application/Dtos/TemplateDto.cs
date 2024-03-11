namespace SmartReader.Core.Application.Dtos;

public class TemplateDto
{
    public int SiteCode { get; set; }
    public string Facility { get; set; }
    public int PatientCount { get; set; }
    public int? VisitCount { get; set; }
    public int? PharmacyCount { get; set; }
}