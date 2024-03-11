using AutoMapper;
using SmartReader.Core.Domain;

namespace SmartReader.Core.Application.Dtos;

public class ExtractHistoryDto
{
    public int Id  { get; set; }
    public int ExtractId { get; set; }
    public string ExtractName { get; set; }
    public long Loaded { get; set; }
    public long? Sent { get; set; }
    public DateTime? Date { get; set; }
    public string? Status { get; set; }
}

public class ReaderProfile:Profile
{
    public ReaderProfile()
    {
        CreateMap<ExtractHistory, ExtractHistoryDto>();
    }
}