using Microsoft.EntityFrameworkCore;
using SmartReader.Core.Domain;
using SmartReader.Core.Domain.Ct;

namespace SmartReader.Core.Application.Interfaces;

public interface ISmartReaderDbContext
{ 
    DbSet<Registry> Registries { get; }
    DbSet<Extract> Extracts { get; }
    DbSet<Config> Configs { get; }
    
    DbSet<Patient> Patients { get; }
    DbSet<PatientPharmacy> PatientPharmacies { get; }
    DbSet<PatientVisit> PatientVisits { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}