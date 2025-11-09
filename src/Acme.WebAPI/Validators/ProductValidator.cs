using Acme.Models.Entity;
using FluentValidation;

namespace Acme.WebAPI.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3);
    }
}