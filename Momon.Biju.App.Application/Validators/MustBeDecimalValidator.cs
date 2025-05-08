using FluentValidation;

namespace Momon.Biju.App.Application.Validators;

public class MustBeDecimalValidator : AbstractValidator<string>
{
    public MustBeDecimalValidator()
    {
        RuleFor(x => x)
            .Custom((prop, context) =>
            {
                if (!decimal.TryParse(prop, out decimal amount))
                {
                    context.AddFailure("Preço inválido");
                }
            });
    }
}