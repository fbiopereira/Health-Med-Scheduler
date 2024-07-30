using FluentValidation;

namespace HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialidadeMedico
{
    public class AdicionarEspecialidadeMedicoCommandValidator : AbstractValidator<AdicionarEspecialidadeMedicoCommand>
    {
        public AdicionarEspecialidadeMedicoCommandValidator()
        {
            RuleFor(x => x.EspecialidadeId)
                .NotEmpty().WithMessage("O id da especialidade não pode ser vazio.");

            RuleFor(x => x.MedicoId)
                .NotEmpty().WithMessage("O id do médico não pode ser vazio.");

            RuleFor(x => x.DataRegistro)
                .NotEmpty().WithMessage("A data de registro não pode ser vazia.");  
        }
    }
   
}
