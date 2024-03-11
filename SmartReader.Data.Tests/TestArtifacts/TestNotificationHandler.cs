using MediatR;
using Serilog;
using SmartReader.Core.Domain.Events;

namespace SmartReader.Data.Tests.TestArtifacts;

public class TestNotificationHandler :
        INotificationHandler<ExtractsSendingStarted>,
        INotificationHandler<ExtractsSendingCompleted>,
        INotificationHandler<ExtractsSendingFailed>,
        INotificationHandler<ExtractSent>,
        INotificationHandler<ExtractSendFail>
        
{
        public Task Handle(ExtractsSendingStarted notification, CancellationToken cancellationToken)
        {
                Log.Information($"Sending to {notification.Registry}:{notification.Url} started");
                Log.Information(new string('*',50));
                return Task.CompletedTask;
        }

        public Task Handle(ExtractsSendingCompleted notification, CancellationToken cancellationToken)
        {
                Log.Information(new string('*',50));
                Log.Information($"Sending to {notification.Registry}:{notification.Url} completed");
                return Task.CompletedTask;
        }

        public Task Handle(ExtractsSendingFailed notification, CancellationToken cancellationToken)
        {
                Log.Information($"Sending Error: {notification.Error} !");
                return Task.CompletedTask;
        }

        public Task Handle(ExtractSent notification, CancellationToken cancellationToken)
        {
                Log.Information($"Sent [{notification.Name}] {notification.Count} record(s) ");
                return Task.CompletedTask;
        }

        public Task Handle(ExtractSendFail notification, CancellationToken cancellationToken)
        {
                Log.Information($"Sending [{notification.Name}] Error:  {notification.Error} !");
                return Task.CompletedTask;
        }
}