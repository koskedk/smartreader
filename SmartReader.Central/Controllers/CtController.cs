using Microsoft.AspNetCore.Mvc;
using SmartReader.Central.Application.Domain;
using SmartReader.Central.Application.Interfaces;

namespace SmartReader.Central.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CtController : ControllerBase
    {
        private readonly ILogger<CtController> _logger;
        private readonly ISmartCentralDbContext _context;
        
        public CtController(ILogger<CtController> logger, ISmartCentralDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("Patient")]
        public async Task<IActionResult> Patient(List<Patient> patients)
        {
            try
            {
                _logger.LogInformation("Saving patients");
                _context.Patients.AddRange(patients);
                await _context.SaveChangesAsync(CancellationToken.None);
                _logger.LogInformation($"saved {patients.Count} record(s)");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e,"save Error");
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("Pharmacy")]
        public async Task<IActionResult> Pharmacy(List<Pharmacy> pharmacies)
        {
            
            try
            {
                _logger.LogInformation("Saving pharmacies");
                _context.Pharmacies.AddRange(pharmacies);
                await _context.SaveChangesAsync(CancellationToken.None);
                _logger.LogInformation($"saved {pharmacies.Count} record(s)");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e,"save Error");
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("Visit")]
        public async Task<IActionResult> Visit(List<Visit> visits)
        {
           
            try
            {
                _logger.LogInformation("Saving visits");
                _context.Visits.AddRange(visits);
                await _context.SaveChangesAsync(CancellationToken.None);
                _logger.LogInformation($"saved {visits.Count} record(s)");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e,"save Error");
                return StatusCode(500, e.Message);
            }
        }
    }
}
