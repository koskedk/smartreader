using MediatR;

namespace SmartReader.Core.Domain.Events;

public class ExtractsScanningFailed:INotification
{
    public string Error { get;  }
    public DateTime When { get;  }=DateTime.Now;

    public ExtractsScanningFailed(string error)
    {
        Error = error;
    }
}