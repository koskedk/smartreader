using Microsoft.EntityFrameworkCore;

namespace SmartReader.Central.Data;

public class SmartCentralDbContextInitializer
{
    private readonly ILogger<SmartCentralDbContextInitializer> _logger;
    private readonly SmartCentralDbContext _context;

    public SmartCentralDbContextInitializer(ILogger<SmartCentralDbContextInitializer> logger, SmartCentralDbContext context)
    {
        _logger = logger;
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
            _logger.LogError(ex, "An error occurred while initialising the database.");
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
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public Task TrySeedAsync()
    {
        return Task.CompletedTask;
        //    await _context.SaveChangesAsync();
    }
}