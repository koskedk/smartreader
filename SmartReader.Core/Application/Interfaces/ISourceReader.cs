using System.Data;
using SmartReader.Core.Domain;

namespace SmartReader.Core.Application.Interfaces;

public interface ISourceReader
{
    Task<long> GetCount(Extract extract);
    IDataReader GetReader(Extract extract);
}