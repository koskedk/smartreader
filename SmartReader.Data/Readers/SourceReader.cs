using System.Data;
using System.Text;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SmartReader.Core.Application.Interfaces;
using SmartReader.Core.Domain;

namespace SmartReader.Data.Readers;

public class SourceReader:ISourceReader
{
    private readonly SmartReaderDbContext _context;

    public SourceReader(SmartReaderDbContext context)
    {
        _context = context;
    }

    public async Task ClearHistory(CancellationToken cancellationToken)
    {
        var tables = new List<string>()
        {
            nameof(SmartReaderDbContext.ExtractHistories)
        };
        var scb = new StringBuilder();
        tables.ForEach(s => scb.AppendLine($"truncate table {s}; "));

        if (_context.Database.IsMySql())
        {
            using (var cn = new MySqlConnection(_context.Database.GetConnectionString()))
            {
                await cn.OpenAsync(cancellationToken);
                await cn.ExecuteAsync(scb.ToString(), commandTimeout: 0);
            }
        }
    }

    public async Task InitializeHistory(CancellationToken cancellationToken)
    {
        var extracts = _context.Extracts.AsNoTracking().ToList();
        var list = new List<ExtractHistory>();
        extracts.ForEach(x => list.Add(new ExtractHistory(0, x.Id)));
        await _context.AddRangeAsync(list, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<long> GetCount(Extract extract,CancellationToken cancellationToken)
    {
        long count = 0;
        
        if (_context.Database.IsMySql())
        {
            using ( var cn =new MySqlConnection(_context.Database.GetConnectionString()))
            {
                await cn.OpenAsync(cancellationToken);
                count = await cn.ExecuteScalarAsync<long>(extract.SqlCount,commandTimeout:0);
            }
        }

        return count;
    }

    public IDataReader GetReader(Extract extract)
    {
        if (_context.Database.IsMySql())
            return GetMySqlDataReader(extract.Sql);
        
        throw new Exception("Database type not supported!");
    }

    public async Task UpdateLoadHistory(int extractId, long count)
    {
        var when = DateTime.Now;
        
        var sql = $@"
            update 
                {nameof(_context.ExtractHistories)} 
            set 
                {nameof(ExtractHistory.Loaded)}=@count,
                {nameof(ExtractHistory.Date)}=@when
            where 
                 {nameof(ExtractHistory.ExtractId)}=@extractId 
           ";
        
        if (_context.Database.IsMySql())
        {
            using (var cn = new MySqlConnection(_context.Database.GetConnectionString()))
            {
                await cn.OpenAsync();
                await cn.ExecuteAsync(sql,new {extractId,count,when});
            }
        }
    }

    public async Task UpdateSentHistory(int extractId, long count)
    {
        var when = DateTime.Now;
        
        var sql = $@"
            update 
                {nameof(_context.ExtractHistories)} 
            set 
                {nameof(ExtractHistory.Sent)}=@count,
                {nameof(ExtractHistory.Date)}=@when
            where 
                 {nameof(ExtractHistory.ExtractId)}=@extractId 
           ";
        
        if (_context.Database.IsMySql())
        {
            using (var cn = new MySqlConnection(_context.Database.GetConnectionString()))
            {
                await cn.OpenAsync();
                await cn.ExecuteAsync(sql,new {extractId,count,when});
            }
        }
    }

    public async Task UpdateStatusHistory(int extractId, string status)
    {
        var when = DateTime.Now;
        
        var sql = $@"
            update 
                {nameof(_context.ExtractHistories)} 
            set 
                {nameof(ExtractHistory.Status)}=@status,
                {nameof(ExtractHistory.Date)}=@when
            where 
                 {nameof(ExtractHistory.ExtractId)}=@extractId 
           ";
        
        if (_context.Database.IsMySql())
        {
            using (var cn = new MySqlConnection(_context.Database.GetConnectionString()))
            {
                await cn.OpenAsync();
                await cn.ExecuteAsync(sql,new {extractId,status,when});
            }
        }
    }

    public async Task Purge(CancellationToken cancellationToken)
    {
        var tables = new List<string>()
        {
            nameof(SmartReaderDbContext.ExtractHistories),
            nameof(SmartReaderDbContext.PatientPharmacies),
            nameof(SmartReaderDbContext.PatientVisits),
            nameof(SmartReaderDbContext.Patients)
        };
        var scb = new StringBuilder();
        tables.ForEach(s => scb.AppendLine($"truncate table {s}; "));

        if (_context.Database.IsMySql())
        {
            using (var cn = new MySqlConnection(_context.Database.GetConnectionString()))
            {
                await cn.OpenAsync(cancellationToken);
                await cn.ExecuteAsync(scb.ToString(), commandTimeout: 0);
            }
        }
    }

    private MySqlDataReader GetMySqlDataReader(string commandSql)
    {
        var cn =new MySqlConnection(_context.Database.GetConnectionString());
        var cmd = new MySqlCommand(commandSql, cn);
        cn.Open();
        var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return reader;
    }
    
}