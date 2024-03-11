using MediatR;

namespace SmartReader.Core.Domain.Events;

public class ExtractsScanningCompleted:INotification
{
    public int Count { get; }
    public DateTime When { get;  }=DateTime.Now;

    public ExtractsScanningCompleted(int count)
    {
        Count = count;
    }
}