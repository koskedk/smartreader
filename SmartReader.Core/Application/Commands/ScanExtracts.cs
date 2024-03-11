using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SmartReader.Core.Application.Interfaces;
using SmartReader.Core.Domain.Events;

namespace SmartReader.Core.Application.Commands;

public class ScanExtracts:IRequest<Result>
{
}

public class ScanExtractsHandler : IRequestHandler<ScanExtracts, Result>
{
    private readonly ISmartReaderDbContext _context;
    private readonly ISourceReader _reader;
    private readonly IMediator _mediator;

    public ScanExtractsHandler(ISmartReaderDbContext context, ISourceReader reader, IMediator mediator)
    {
        _context = context;
        _reader = reader;
        _mediator = mediator;
    }

    public async Task<Result> Handle(ScanExtracts request, CancellationToken cancellationToken)
    {
        try
        {
            var extracts = _context.Extracts.AsNoTracking().ToList();
            
            await _mediator.Publish(new ExtractsScanningStarted(extracts.Count),cancellationToken);
            
            
            foreach (var extract in extracts)
            {
                var count = await _reader.GetCount(extract, cancellationToken);
                await _mediator.Publish(new ExtractScanned(extract.Id, extract.Name, count), cancellationToken);
            }
            
            await _mediator.Publish(new ExtractsScanningCompleted(extracts.Count), cancellationToken);
            return Result.Success();
        }
        catch (Exception e)
        {
            Log.Error(e,"Scan error!");
            await _mediator.Publish(new ExtractsScanningFailed(e.Message), cancellationToken);
            return Result.Failure(e.Message);
        }
    }
}