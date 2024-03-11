using System.Data;
using CSharpFunctionalExtensions;
using SmartReader.Core.Domain;

namespace SmartReader.Core.Application.Interfaces;

public interface ISourceReader
{
    Task ClearHistory(CancellationToken cancellationToken);
    Task InitializeHistory(CancellationToken cancellationToken);
    Task<long> GetCount(Extract extract,CancellationToken cancellationToken);
    IDataReader GetReader(Extract extract);
    Task UpdateLoadHistory(int extractId, long count);
    Task UpdateSentHistory(int extractId, long count);
    Task UpdateStatusHistory(int extractId, string status);
}