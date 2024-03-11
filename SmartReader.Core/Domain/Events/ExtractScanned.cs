using MediatR;

namespace SmartReader.Core.Domain.Events;

public class ExtractScanned:INotification
{
    public int Id { get;  }
    public string Name { get;  }
    public long Count { get;  }
    public DateTime When { get;  }=DateTime.Now;

    public ExtractScanned(int id, string name, long count)
    {
        Id = id;
        Name = name;
        Count = count;
    }
}