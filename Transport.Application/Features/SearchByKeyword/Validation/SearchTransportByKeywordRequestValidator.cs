using FluentValidation;

namespace Transport.Application.Features.SearchByKeyword.Validation;

public class SearchTransportByKeywordRequestValidator : AbstractValidator<SearchTransportByKeywordRequest>
{
    public SearchTransportByKeywordRequestValidator()
    {
        RuleFor(request => request.Keyword)
            .NotEmpty()
            .WithMessage("Keyword must not be empty.");
    }
}
