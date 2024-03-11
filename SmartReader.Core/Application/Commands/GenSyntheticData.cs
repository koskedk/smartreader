using CSharpFunctionalExtensions;
using FizzWare.NBuilder;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SmartReader.Core.Application.Dtos;
using SmartReader.Core.Application.Interfaces;
using SmartReader.Core.Application.Queries;
using SmartReader.Core.Domain.Ct;
using SmartReader.Core.Domain.Events;

namespace SmartReader.Core.Application.Commands;

public class GenSyntheticData:IRequest<Result<List<ExtractHistoryDto>>>
{
    public int SiteCode { get; }
    public string Facility { get; }
    public int PatientCount { get; }
    public int? VisitCount { get; }
    public int? PharmacyCount { get; }

    public GenSyntheticData(int siteCode, string facility, int count, int? visitCount = null, int? pharmacyCount = null)
    {
        PatientCount = count;
        SiteCode = siteCode;
        Facility = facility;
        VisitCount = visitCount.HasValue && visitCount.Value > 0 ? visitCount.Value : count * 5;
        PharmacyCount = pharmacyCount.HasValue && pharmacyCount.Value > 0 ? pharmacyCount.Value : count * 3;
    }

    public GenSyntheticData(TemplateDto dto):
        this(dto.SiteCode,dto.Facility,dto.PatientCount,dto.VisitCount,dto.PharmacyCount)
    {
    }
}

public class GenSyntheticDataHandler : IRequestHandler<GenSyntheticData, Result<List<ExtractHistoryDto>>>
{
    private readonly IMediator _mediator;
    private ISmartReaderDbContext _context;
    private readonly ISourceReader _reader;

    public GenSyntheticDataHandler(IMediator mediator, ISmartReaderDbContext context, ISourceReader reader)
    {
        _mediator = mediator;
        _context = context;
        _reader = reader;
    }

    public async Task<Result<List<ExtractHistoryDto>>> Handle(GenSyntheticData request, CancellationToken cancellationToken)
    {
        try
        {
            await _reader.Purge(cancellationToken);
            
            var patients = GetTestPatients(request.PatientCount, request.SiteCode, request.Facility);
            await _context.BulkInsert(patients);
            var visits = GetTestVisits(patients,  request.VisitCount.Value,request.SiteCode, request.Facility);
            await _context.BulkInsert(visits);
            var pharmacies = GetTestPharmacies(patients,  request.PharmacyCount.Value,request.SiteCode, request.Facility);
            await _context.BulkInsert(pharmacies);

            await _reader.InitializeHistory(cancellationToken);
            foreach (var extract in _context.Extracts.AsNoTracking().ToList())
            {
                var count = await _reader.GetCount(extract, cancellationToken);
                await _reader.UpdateLoadHistory(extract.Id, count);
            }

            var res =await _mediator.Send(new GetHistory(),cancellationToken);
            
            if (res.IsSuccess)
                return Result.Success(res.Value);
            
            throw new Exception(res.Error);
        }
        catch (Exception e)
        {
            Log.Error(e,"Gen synthetic error");
            return Result.Failure<List<ExtractHistoryDto>>(e.Message);
        }
    }
    
    
    private  List<Patient> GetTestPatients(int count , int siteCode, string facility)
    {
        var data = Builder<Patient>.CreateListOfSize(count)
            .All()
            .With(x => x.Id =Guid.NewGuid())
            .With(x => x.SiteCode = siteCode)
            .With(x => x.FacilityName = facility);

        return data.Build().ToList();
    }
    private List<PatientVisit> GetTestVisits(List<Patient> patients,int count, int siteCode, string facility)
    {
        int max = patients.MaxBy(x => x.PatientPK).PatientPK;
        int min = patients.MinBy(x => x.PatientPK).PatientPK;
        
        var data = Builder<PatientVisit>.CreateListOfSize(count)
            .All()
            .With(x => x.Id =Guid.NewGuid())
            .With(x => x.SiteCode = siteCode)
            .With(x=>x.PatientPK=GetRandomNumber(min,max));

        return data.Build().ToList();
    }
    private  List<PatientPharmacy> GetTestPharmacies(List<Patient> patients, int count, int siteCode, string facility )
    {
        int max = patients.MaxBy(x => x.PatientPK).PatientPK;
        int min = patients.MinBy(x => x.PatientPK).PatientPK;
        
        var data = Builder<PatientPharmacy>.CreateListOfSize(count)
            .All()
            .With(x => x.Id =Guid.NewGuid())
            .With(x => x.SiteCode = siteCode)
            .With(x=>x.PatientPK=GetRandomNumber(min,max));;

        return data.Build().ToList();
    }
    
    private  int GetRandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max + 1);
    }
}