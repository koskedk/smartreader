using CSharpFunctionalExtensions;
using MediatR;
using Serilog;
using SmartReader.Core.Application.Interfaces;

namespace SmartReader.Core.Application.Commands;

public enum Action
{
    OnInit,OnScan,OnSend,OnStatus
}

public class UpdateHistory:IRequest<Result>
{
    public Action Action { get; }
    public int? ExtractId { get; } 
    public long? Count { get; }
    public string? Status { get; }

    public UpdateHistory(Action action,int? extractId=null,long? count=null,string? status=null)
    {
        Action = action;
        ExtractId = extractId;
        Count = count;
        Status = status;
    }
}

public class UpdateHistoryHandler:IRequestHandler<UpdateHistory,Result>
{
    private ISourceReader _sourceReader;

    public UpdateHistoryHandler(ISmartReaderDbContext context, ISourceReader sourceReader)
    {
        _sourceReader = sourceReader;
    }

    public async Task<Result> Handle(UpdateHistory request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Action == Action.OnInit)
            {
                await _sourceReader.ClearHistory(CancellationToken.None);
                await _sourceReader.InitializeHistory(CancellationToken.None);
            }
            if (request.Action == Action.OnScan)
            {
                await _sourceReader.UpdateLoadHistory(request.ExtractId.Value, request.Count.Value);
            }
            if (request.Action == Action.OnSend)
            {
                await _sourceReader.UpdateSentHistory(request.ExtractId.Value, request.Count.Value);
            }
            if (request.Action == Action.OnStatus)
            {
                await _sourceReader.UpdateStatusHistory(request.ExtractId.Value, request.Status);
            }

            return Result.Success();
        }
        catch (Exception e)
        {
            Log.Error(e, "Update history error");
            return Result.Failure(e.Message);
        }
    }
}