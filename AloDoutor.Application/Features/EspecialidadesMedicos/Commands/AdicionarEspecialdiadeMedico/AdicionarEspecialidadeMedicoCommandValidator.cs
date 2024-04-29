using FluentValidation;
using System.Text.RegularExpressions;

namespace AloDoutor.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialdiadeMedico
{
    public class AdicionarEspecialidadeMedicoCommandValidator : AbstractValidator<AdicionarEspecialidadeMedicoCommand>
    {
        public AdicionarEspecialidadeMedicoCommandValidator()
        {
            RuleFor(x => x.EspecialidadeId).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
            RuleFor(x => x.MedicoId).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
            RuleFor(x => x.DataRegistro).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}
