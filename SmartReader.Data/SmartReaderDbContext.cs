using Microsoft.EntityFrameworkCore;
using SmartReader.Core.Application.Interfaces;
using SmartReader.Core.Domain;
using SmartReader.Core.Domain.Ct;

namespace SmartReader.Data;

public class SmartReaderDbContext:DbContext, ISmartReaderDbContext
{
    public DbSet<Registry> Registries => Set<Registry>();
    public DbSet<Extract> Extracts => Set<Extract>();
    public DbSet<Config> Configs => Set<Config>();
    public DbSet<ExtractHistory> ExtractHistories => Set<ExtractHistory>();
    
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<PatientPharmacy> PatientPharmacies => Set<PatientPharmacy>();
    public DbSet<PatientVisit> PatientVisits => Set<PatientVisit>();

    public SmartReaderDbContext(DbContextOptions<SmartReaderDbContext> options) : base(options)
    {
    }
}