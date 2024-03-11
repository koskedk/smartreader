using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SmartReader.Core.Application.Interfaces;
using SmartReader.Core.Domain.Ct;
using SmartReader.Core.Domain.Events;

namespace SmartReader.Core.Application.Commands;

public class SendExtracts:IRequest<Result>
{
    public int RegistryId { get; }

    public SendExtracts(int registryId)
    {
        RegistryId = registryId;
    }
}

public class SendExtractHandler:IRequestHandler<SendExtracts,Result>
{
    private readonly IMediator _mediator;
    private readonly ISmartReaderDbContext _context;
    private readonly ISendService _sendService;

    public SendExtractHandler(IMediator mediator, ISmartReaderDbContext context, ISendService sendService)
    {
        _mediator = mediator;
        _context = context;
        _sendService = sendService;
    }

    public async Task<Result> Handle(SendExtracts request, CancellationToken cancellationToken)
    {
        try
        {
            // configs
            var configs = _context.Configs.AsNoTracking().ToList();
            var batch = configs.FirstOrDefault(x => x.Id.ToLower() == "send.batch.size".ToLower());
            int batchSize = batch?.GetNumericValue() ?? 100;
            
            // registry 
            var registry =await  _context.Registries.FindAsync(request.RegistryId);
            if (registry is null)
                throw new Exception("Registry not found or configured!");
            
            // extracts 
            var extracts = _context.Extracts.AsNoTracking().ToList();
            if (!extracts.Any())
                throw new Exception("Extracts not found !");
            
            await _mediator.Publish(new ExtractsSendingStarted(registry.Display, registry.Url),cancellationToken);
            
            // Main Extract
            var patientExtract = extracts.First(x => x.Name == "Patient");
            await _sendService.Send<Patient>(registry, patientExtract, batchSize, cancellationToken);
            
            // Other Extracts
            var sentTasks = new List<Task>();
            
            foreach (var extract in extracts.OrderBy(x=>!x.IsPriority))
            {
             
                if (extract.Name == "Pharmacy")
                    sentTasks.Add(_sendService.Send<PatientPharmacy>(registry, extract, batchSize, cancellationToken));
                
                if (extract.Name == "Visit")
                    sentTasks.Add(_sendService.Send<PatientVisit>(registry, extract, batchSize, cancellationToken));
            }

            await Task.WhenAll(sentTasks);
            
            await _mediator.Publish(new ExtractsSendingCompleted(registry.Display, registry.Url),cancellationToken);
            return Result.Success();
        }
        catch (Exception e)
        {
            await _mediator.Publish(new ExtractsSendingFailed(request.RegistryId, e.Message),cancellationToken);
            Log.Error(e,"Send Failed! ");
            return Result.Failure(e.Message);
        }
    }
}