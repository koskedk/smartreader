using MediatR;

namespace SmartReader.Core.Domain.Events;

public class ExtractsSendingStarted:INotification
{
    public string Registry { get;  }
    public string Url { get;  }
    public DateTime When { get;  }=DateTime.Now;

    public ExtractsSendingStarted(string registry, string url)
    {
        Registry = registry;
        Url = url;
    }
}