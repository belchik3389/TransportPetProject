using FluentValidation;
using Transport.Domain.Contracts;

namespace Transport.Application.Features.CreateTransport.Validation;

public class CreateTransportRequestValidation : AbstractValidator<CreateTransportRequest>
{
    public CreateTransportRequestValidation(ITransportRepository transportRepository)
    {
        RuleFor(request => request.NumberPlate)
            .NotEmpty()
            .WithMessage("Number plate must not be empty.");

        RuleFor(request => request.MaxPassengersCount)
            .GreaterThan((byte)0)
            .WithMessage("Max passengers count must be greater than zero.");

        RuleFor(request => request.TypeId)
            .NotEmpty()
            .WithMessage("Type id must not be empty.")
            .MustAsync(async (typeId, cancellationToken) =>
                await transportRepository.GetByTypeId(typeId, cancellationToken) is not null)
            .WithMessage(request => $"Transport type with id '{request.TypeId}' was not found.");
    }
}
