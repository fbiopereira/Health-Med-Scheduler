using FluentValidation;

namespace AloDoutor.Application.Features.EspecialidadesMedicos.Commands.AtualizarEspecialidadeMedico
{
    public class AtualizarEspecialidadeMedicoCommandValidator : AbstractValidator<AtualizarEspecialidadeMedicoCommand>
    {
        public AtualizarEspecialidadeMedicoCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
            RuleFor(x => x.EspecialidadeId).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
            RuleFor(x => x.MedicoId).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
            RuleFor(x => x.DataRegistro).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}
