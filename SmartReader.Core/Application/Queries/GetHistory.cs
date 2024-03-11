using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SmartReader.Core.Application.Dtos;
using SmartReader.Core.Application.Interfaces;

namespace SmartReader.Core.Application.Queries;

public class GetHistory:IRequest<Result<List<ExtractHistoryDto>>>
{
}

public class GetHistoryHandler : IRequestHandler<GetHistory, Result<List<ExtractHistoryDto>>>
{
    private readonly ISmartReaderDbContext _context;
    private readonly IMapper _mapper;

    public GetHistoryHandler(ISmartReaderDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<ExtractHistoryDto>>> Handle(GetHistory request, CancellationToken cancellationToken)
    {
        try
        {
            var extractHistoryDtos = new List<ExtractHistoryDto>();
            
            var extracts = await _context.Extracts
                .Include(x => x.Histories)
                .ToListAsync(cancellationToken);

            foreach (var extract in extracts)
            {
                var dtos = _mapper.Map<List<ExtractHistoryDto>>(extract.Histories);
                dtos.ForEach(dto =>
                {
                    dto.ExtractId = extract.Id;
                    dto.ExtractName = extract.Name;
                });
                extractHistoryDtos.AddRange(dtos);
            }

            return Result.Success(extractHistoryDtos);

        }
        catch (Exception e)
        {
            Log.Error(e,"Get History Error");
            return Result.Failure<List<ExtractHistoryDto>>(e.Message);
        }
    }
}