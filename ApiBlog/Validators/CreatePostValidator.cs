using ApiBlog.DTOs;
using FluentValidation;

namespace ApiBlog.Validators
{
    public class CreatePostValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Título é obrigatorio.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Conteúdo é obrigatório.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Usuário é obrigatório.");
        }
    }
}
