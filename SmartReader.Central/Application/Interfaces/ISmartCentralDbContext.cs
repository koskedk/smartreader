using Microsoft.EntityFrameworkCore;
using SmartReader.Central.Application.Domain;

namespace SmartReader.Central.Application.Interfaces;

public interface ISmartCentralDbContext
{ 
    DbSet<Patient> Patients { get; }
    DbSet<Pharmacy> Pharmacies { get; }
    DbSet<Visit> Visits { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}