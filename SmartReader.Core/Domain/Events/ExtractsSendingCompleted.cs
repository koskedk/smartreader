using MediatR;

namespace SmartReader.Core.Domain.Events;

public class ExtractsSendingCompleted:INotification
{
    public string Registry { get;  }
    public string Url { get;  }
    public DateTime When { get;  }=DateTime.Now;

    public ExtractsSendingCompleted(string registry, string url)
    {
        Registry = registry;
        Url = url;
    }
}