using FluentValidation;
using System.Text.RegularExpressions;

namespace AloDoutor.Application.Features.Especialidades.Commands.RemoverEspecialidade
{
    public class RemoverEspecialidadeCommandValidator : AbstractValidator<RemoverEspecialidadeCommand>
    {
        public RemoverEspecialidadeCommandValidator()
        {
            RuleFor(c => c.idEspecialidade)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

        }
    }
}
