using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transport.Application.Features.GetById;
using Transport.Application.Features.SearchByKeyword;

namespace Transport.Api.Controllers;

[ApiController]
[Route("transport")]
public class TransportController(IMediator mediator) : ControllerBase
{
    // TODO: ActionResult убрал бы и проверку тогда.
    // сразу await mediator.Send(new GetTransportByIdRequest(id));
    // Task<TransportByIdResponse>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TransportByIdResponse>> GetById(Guid id)
    {
        var response = await mediator.Send(new GetTransportByIdRequest(id));

        if (response == null)
        {
            return NotFound();
        }

        return response;
    }
    
    [HttpGet("searchByKeyword")]
    public async Task<List<SearchTransportByKeywordResponse>> SearchByKeyword([FromQuery] string keyword)
    {
        return await mediator.Send(new SearchTransportByKeywordRequest(keyword));
    }
}
