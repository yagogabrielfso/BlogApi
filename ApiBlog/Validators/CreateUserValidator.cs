using ApiBlog.DTOs;
using FluentValidation;

namespace ApiBlog.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username é obrigatório");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password é obrigatório")
                .MinimumLength(10).WithMessage("Password precisa ter no mínimo 10 caracteres.");
        }

    }
}
