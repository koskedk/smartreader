using System.Collections;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartReader.Core.Application.Commands;
using SmartReader.Core.Application.Dtos;

namespace SmartReader.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SetupController : ControllerBase
{
    private readonly IMediator _mediator;

    public SetupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Synthetic")]
    [ProducesResponseType(typeof(IEnumerable<ExtractHistoryDto>), 200)]
    public async Task<IActionResult> GenData(TemplateDto templateDto)
    {
        try
        {
            var res = await _mediator.Send(new GenSyntheticData(templateDto));

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