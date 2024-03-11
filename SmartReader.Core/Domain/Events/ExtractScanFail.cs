using MediatR;

namespace SmartReader.Core.Domain.Events;

public class ExtractScanFail:INotification
{
    public int Id { get;  }
    public string Name { get;  }
    public string Error { get;  }
    public DateTime When { get;  }=DateTime.Now;
    
    public ExtractScanFail(int id, string name, string error)
    {
        Id = id;
        Name = name;
        Error = error;
    }
}