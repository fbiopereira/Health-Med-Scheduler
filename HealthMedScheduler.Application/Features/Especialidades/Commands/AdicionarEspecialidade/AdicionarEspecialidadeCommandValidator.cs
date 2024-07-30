using FluentValidation;

namespace HealthMedScheduler.Application.Features.Especialidades.Commands.AdicionarEspecialidade
{
    public class AdicionarEspecialidadeCommandValidator : AbstractValidator<AdicionarEspecialidadeCommand>
    {
        public AdicionarEspecialidadeCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome da especialidade não pode ser vazio.")
                .MaximumLength(100).WithMessage("O nome da especialidade não pode ter mais de 100 caracteres.");

            RuleFor(x => x.Descricao)
                .MaximumLength(500).WithMessage("A descrição da especialidade não pode ter mais de 500 caracteres.");
        }
    }
}
