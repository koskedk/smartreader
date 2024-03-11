using System.Data;
using System.Net.Http.Json;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SmartReader.Core.Application.Interfaces;
using SmartReader.Core.Domain;
using SmartReader.Core.Domain.Events;

namespace SmartReader.Data.Services;

public class SendService : ISendService
{
    private readonly SmartReaderDbContext _context;
    private readonly IMediator _mediator;
    private HttpClient _httpClient;

    public SendService(SmartReaderDbContext context, IMediator mediator, HttpClient httpClient)
    {
        _context = context;
        _mediator = mediator;
        _httpClient = httpClient;
    }

    public async Task<Result> Send<T>(Registry registry, Extract extract, int batchSize, CancellationToken cancellationToken)
    {
        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        
        var url = $"{registry.Url}{extract.EndPoint}";
        var connectionString = _context.Database.GetConnectionString();
        int totalCount = 0;
        var package = new List<T>();

        if (_context.Database.IsMySql())
        {
            using (var cn = GetMySqlConnection(connectionString))
            {
                await cn.OpenAsync(cancellationToken);
                using (var cmd = GetMySqlCommand(cn, extract.Sql))
                {
                    using (var reader =
                           await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken))
                    {
                        int count = 0;
                        var parser = reader.GetRowParser<T>(typeof(T));

                        while (await reader.ReadAsync(cancellationToken))
                        {
                            if (count == batchSize)
                            {
                                // send
                                var content = JsonConvert.SerializeObject(package,new JsonSerializerSettings
                                {
                                    ContractResolver = contractResolver,
                                    Formatting = Formatting.Indented
                                });
                                var res = await _httpClient.PostAsJsonAsync(url, content, cancellationToken);
                                if (res.IsSuccessStatusCode)
                                {
                                    await _mediator.Publish(new ExtractSent(extract.Id, extract.Name, totalCount),
                                        cancellationToken);
                                }
                                else
                                {
                                    var error = await res.Content.ReadAsStringAsync(cancellationToken);
                                    await _mediator.Publish(new ExtractSendFail(extract.Id, extract.Name, error),
                                        cancellationToken);

                                    throw new Exception($"Error sending {extract.Name} : {error}");
                                }

                                // reset
                                count = 0;
                                package = new List<T>();
                            }

                            var patient = parser(reader);
                            package.Add(patient);
                            count++;
                            totalCount++;
                        }

                        if (count > 0)
                        {
                            
                            // send
                            var content = JsonConvert.SerializeObject(new {patients=package},
                                new JsonSerializerSettings
                                {
                                    ContractResolver = contractResolver,
                                    Formatting = Formatting.Indented
                                });
                            var res = await _httpClient.PostAsJsonAsync(url, content, cancellationToken);
                            if (res.IsSuccessStatusCode)
                            {
                                await _mediator.Publish(new ExtractSent(extract.Id, extract.Name, totalCount),
                                    cancellationToken);
                            }
                            else
                            {
                                var error = await res.Content.ReadAsStringAsync(cancellationToken);
                                await _mediator.Publish(new ExtractSendFail(extract.Id, extract.Name, error),
                                    cancellationToken);

                                throw new Exception($"Error sending {extract.Name} : {error}");
                            }

                        }
                    }
                }
            }
        }
        return Result.Success();
    }

    private MySqlConnection GetMySqlConnection(string conStr)
    {
        return new MySqlConnection(conStr);
    }

    private MySqlCommand GetMySqlCommand(MySqlConnection cn, string commandScript)
    {
        return new MySqlCommand(commandScript, cn);
    }
}