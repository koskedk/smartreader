using MediatR;
using SmartReader.Core.Application.Commands;
using SmartReader.Core.Domain.Events;

namespace SmartReader.Core.Application.ProcessManagers;

public class ExtractEventsPm:
    INotificationHandler<ExtractsSendingStarted>,
    INotificationHandler<ExtractSent>,
    INotificationHandler<ExtractScanned>,
    INotificationHandler<ExtractSendFail>
{
    private readonly IMediator _mediator;

    public ExtractEventsPm(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(ExtractsSendingStarted notification, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateHistory(Commands.Action.OnInit), cancellationToken);
    }

    public async Task Handle(ExtractSent notification, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateHistory(Commands.Action.OnSend,notification.Id,notification.Count), cancellationToken);
    }

    public async Task Handle(ExtractSendFail notification, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateHistory(Commands.Action.OnStatus,notification.Id,null,notification.Error), cancellationToken);
    }

    public async Task Handle(ExtractScanned notification, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateHistory(Commands.Action.OnScan,notification.Id,notification.Count), cancellationToken);
    }
}