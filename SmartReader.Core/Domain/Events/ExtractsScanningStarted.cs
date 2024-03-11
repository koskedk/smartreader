using MediatR;

namespace SmartReader.Core.Domain.Events;

public class ExtractsScanningStarted : INotification
{
    public int Count { get; }
    public DateTime When { get; } = DateTime.Now;

    public ExtractsScanningStarted(int count)
    {
        Count = count;
    }
}