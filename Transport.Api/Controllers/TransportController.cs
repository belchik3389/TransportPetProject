using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transport.Application.Features.CreateTransport;
using Transport.Application.Features.GetById;
using Transport.Application.Features.SearchByKeyword;

namespace Transport.Api.Controllers;

[ApiController]
[Route("transport")]
public class TransportController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TransportByIdResponse>> GetById(Guid id)
    {
        var response = await mediator.Send(new GetTransportByIdRequest(id));

        return response is null ? NotFound() : Ok(response);
    }
    
    [HttpGet("searchByKeyword")]
    public async Task<ActionResult<List<SearchTransportByKeywordResponse>>> SearchByKeyword([FromQuery] string keyword)
    {
        var response = await mediator.Send(new SearchTransportByKeywordRequest(keyword));
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateTransportRequest request)
    {
        var response = await mediator.Send(request);
        
        return CreatedAtAction(nameof(GetById), new { id = response }, response);
    }
}
