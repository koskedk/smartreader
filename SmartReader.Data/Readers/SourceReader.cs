using System.Data;
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

    public async Task<long> GetCount(Extract extract)
    {
        long count = 0;
        
        if (_context.Database.IsMySql())
        {
            using ( var cn =new MySqlConnection(_context.Database.GetConnectionString()))
            {
                cn.Open();
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
    
    private MySqlDataReader GetMySqlDataReader(string commandSql)
    {
        var cn =new MySqlConnection(_context.Database.GetConnectionString());
        var cmd = new MySqlCommand(commandSql, cn);
        cn.Open();
        var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return reader;
    }
    
}