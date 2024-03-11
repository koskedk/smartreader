using FizzWare.NBuilder;
using SmartReader.Core.Domain.Ct;

namespace SmartReader.Data.Tests.TestArtifacts;

public class TestData
{
    public static List<Patient> GetTestPatients(int count = 5, int siteCode = 10000, string facility = "Demo Facility")
    {
        var data = Builder<Patient>.CreateListOfSize(count)
            .All()
            .With(x => x.Id =Guid.NewGuid())
            .With(x => x.SiteCode = siteCode)
            .With(x => x.FacilityName = facility);

        return data.Build().ToList();
    }
    public static List<PatientVisit> GetTestVisits(List<Patient> patients,int count = 20, int siteCode = 10000, string facility = "Demo Facility")
    {
        int max = patients.MaxBy(x => x.PatientPK).PatientPK;
        int min = patients.MinBy(x => x.PatientPK).PatientPK;
        
        var data = Builder<PatientVisit>.CreateListOfSize(count)
            .All()
            .With(x => x.Id =Guid.NewGuid())
            .With(x => x.SiteCode = siteCode)
            .With(x=>x.PatientPK=GetRandomNumber(min,max));

        return data.Build().ToList();
    }
    
    public static List<PatientPharmacy> GetTestPharmacies(List<Patient> patients, int count = 10, int siteCode = 10000, string facility = "Demo Facility")
    {
        int max = patients.MaxBy(x => x.PatientPK).PatientPK;
        int min = patients.MinBy(x => x.PatientPK).PatientPK;
        
        var data = Builder<PatientPharmacy>.CreateListOfSize(count)
            .All()
            .With(x => x.Id =Guid.NewGuid())
            .With(x => x.SiteCode = siteCode)
            .With(x=>x.PatientPK=GetRandomNumber(min,max));;

        return data.Build().ToList();
    }
    
    static int GetRandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max + 1);
    }
}