using FluentValidation;

namespace HealthMedScheduler.Application.Features.Agendamentos.Commands.AdicionarAgendamento
{
    public class AdicionarAgendamentoCommandValidator : AbstractValidator<AdicionarAgendamentoCommand>
    {
        public AdicionarAgendamentoCommandValidator()
        {
            RuleFor(x => x.EspecialidadeMedicoId)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.PacienteId)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.DataHoraAtendimento)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}
