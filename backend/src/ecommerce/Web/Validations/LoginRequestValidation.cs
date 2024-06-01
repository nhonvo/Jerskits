using ecommerce.Application.Common.Models.User;
using FluentValidation;

namespace ecommerce.Web.Validations;

public class LoginRequestValidation : AbstractValidator<LoginRequest>
{
    public LoginRequestValidation()
    {
        RuleFor(x => x.Email).NotEmpty().MaximumLength(100);
    }
}
