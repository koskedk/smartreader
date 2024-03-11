using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartReader.Core.Application.Dtos;
using SmartReader.Core.Application.Queries;

namespace SmartReader.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public HistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Extracts")]
    [ProducesResponseType(typeof(IEnumerable<ExtractHistoryDto>), 200)]
    public async Task<IActionResult> GetHistory()
    {
        try
        {
            var res = await _mediator.Send(new GetHistory());

            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            
            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}