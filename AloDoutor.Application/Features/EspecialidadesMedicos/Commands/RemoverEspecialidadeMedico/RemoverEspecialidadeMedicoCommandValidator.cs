using FluentValidation;

namespace AloDoutor.Application.Features.EspecialidadesMedicos.Commands.RemoverEspecialidadeMedico
{
    public class RemoverEspecialidadeMedicoCommandValidator : AbstractValidator<RemoverEspecialidadeMedicoCommand>
    {
        public RemoverEspecialidadeMedicoCommandValidator()
        {
            RuleFor(c => c.IdEspecialdiadeMedico)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

        }
    }
}
