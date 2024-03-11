namespace SmartReader.Contracts
{
    public interface IPatient:IPackage
    {
        int PatientPK { get; set; }
        int SiteCode { get; set; }
        string? PatientID { get; set; }
        string? NUPI { get; set; }
        string? FacilityName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? RegistrationDate { get; set; }
        string? Pkv { get; set; }
        string? Occupation { get; set; }
    }
}