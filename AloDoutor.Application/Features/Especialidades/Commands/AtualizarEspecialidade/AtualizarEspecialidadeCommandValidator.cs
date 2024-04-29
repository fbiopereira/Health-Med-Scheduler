using FluentValidation;
using System.Text.RegularExpressions;

namespace AloDoutor.Application.Features.Especialidades.Commands.AtualizarEspecialidade
{
    public class AtualizarEspecialidadeCommandValidator : AbstractValidator<AtualizarEspecialidadeCommand>
    {
        public AtualizarEspecialidadeCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.Nome).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(100).WithMessage("O nome da especialidade não pode ter mais de 100 caracteres.");

            RuleFor(x => x.Descricao)
                .MaximumLength(500).WithMessage("A descrição da especialidade não pode ter mais de 500 caracteres.");
        }
    }
}
