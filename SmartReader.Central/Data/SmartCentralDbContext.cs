using Microsoft.EntityFrameworkCore;
using SmartReader.Central.Application.Domain;
using SmartReader.Central.Application.Interfaces;

namespace SmartReader.Central.Data;

public class SmartCentralDbContext:DbContext, ISmartCentralDbContext
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Pharmacy> Pharmacies => Set<Pharmacy>();
    public DbSet<Visit> Visits => Set<Visit>();

    public SmartCentralDbContext(DbContextOptions<SmartCentralDbContext> options) : base(options)
    {
    }
}