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
    public async Task<TransportByIdResponse?> GetById(Guid id)
    {
        return await mediator.Send(new GetTransportByIdRequest(id));
    }
    
    [HttpGet("searchByKeyword")]
    public async Task<IReadOnlyList<SearchTransportByKeywordResponse>> SearchByKeyword([FromQuery] string keyword)
    {
        return await mediator.Send(new SearchTransportByKeywordRequest(keyword));
    }

    [HttpPost]
    public async Task<Guid> Create([FromBody] CreateTransportRequest request)
    {
        return await mediator.Send(request);
    }
}
