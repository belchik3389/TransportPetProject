using FluentValidation;

namespace Transport.Application.Features.CreateTransport.Validation;

public class CreateTransportRequestValidation : AbstractValidator<CreateTransportRequest>
{
    public CreateTransportRequestValidation()
    {
        RuleFor(request => request.NumberPlate)
            .NotEmpty();

        RuleFor(request => request.MaxPassengersCount)
            .GreaterThan((byte)0);

        RuleFor(request => request.TypeId)
            .NotEmpty()
            .IsInEnum();
    }
}
