using Microsoft.EntityFrameworkCore;
using Serilog;
using SmartReader.Core.Domain;

namespace SmartReader.Data;

public class SmartReaderDbContextInitializer
{
    private readonly SmartReaderDbContext _context;

    public SmartReaderDbContextInitializer(SmartReaderDbContext context)
    {
        _context = context;
    }
    
    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
    
    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        
        if (!_context.Registries.Any())
        {
            _context.Registries.Add(new Registry()
            {
                Id = 1, Display = "C&T", Url = "https://localhost:7267"
            });
        }
        
        if (!_context.Extracts.Any())
        {
            _context.Extracts.Add(new Extract
            {
                Id = 1,EndPoint = "/api/Ct/Patient",IsPriority = true,Name= "Patient",Sql = $"select * from {nameof(SmartReaderDbContext.Patients)}" 
            });
            _context.Extracts.Add(new Extract
            {
                Id = 2,EndPoint = "/api/Ct/Pharmacy",IsPriority = false,Name= "Pharmacy",Sql = $"select * from {nameof(SmartReaderDbContext.PatientPharmacies)}"
            });
            _context.Extracts.Add(new Extract
            {
                Id = 3,EndPoint = "/api/Ct/Visit",IsPriority = false,Name= "Visit",Sql = $"select * from {nameof(SmartReaderDbContext.PatientVisits)}"
            });
        }
        
        if (!_context.Configs.Any())
        {
            _context.Configs.Add(new Config()
            {
                Id = "send.batch.size",Description = "Post batch size",Type = ConfigType.Numeric,Value = "50"
            });
        }
        
        await _context.SaveChangesAsync();
    }
}