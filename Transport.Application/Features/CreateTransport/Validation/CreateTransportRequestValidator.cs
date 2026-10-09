using FluentValidation;
using Transport.Application.Enums;

namespace Transport.Application.Features.CreateTransport.Validation;

public class CreateTransportRequestValidator : AbstractValidator<CreateTransportRequest>
{
    public CreateTransportRequestValidator()
    {
        RuleFor(request => request.NumberPlate)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(request => request.MaxPassengersCount)
            .GreaterThan((byte)0);

        RuleFor(request => request.TypeId)
            .Must(typeId => Enum.IsDefined((TransportType)typeId))
            .WithMessage("Unknown transport type.");
    }
}
