using MediatR;

namespace SmartReader.Core.Domain.Events;

public class ExtractsSendingFailed:INotification
{
    public int RegistryId { get;  }
    public string Error { get;  }
    public DateTime When { get;  }=DateTime.Now;
    
    public ExtractsSendingFailed(int registryId,  string error)
    {
        RegistryId = registryId;
        Error = error;
    }
}