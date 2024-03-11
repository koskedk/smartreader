using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartReader.Core.Application.Commands;
using SmartReader.Core.Application.Dtos;

namespace SmartReader.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransmitController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransmitController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Scan")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> Scan()
    {
      
        try
        {
            var res = await _mediator.Send(new ScanExtracts());

            if (res.IsSuccess)
            {
                return Ok();
            }
            
            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("SendAll")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> Send(SendDto dto)
    {
        try
        {
            var res = await _mediator.Send(new SendExtracts(dto));

            if (res.IsSuccess)
            {
                return Ok();
            }
            
            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}