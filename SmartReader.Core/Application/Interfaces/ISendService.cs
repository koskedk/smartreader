using CSharpFunctionalExtensions;
using SmartReader.Core.Domain;

namespace SmartReader.Core.Application.Interfaces;

public interface ISendService
{
    Task<Result> Send<T>(Registry registry,Extract extract,int batchSize,CancellationToken cancellationToken);
}