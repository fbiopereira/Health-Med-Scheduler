using FluentValidation;

namespace HealthMedScheduler.Application.Features.Agendamentos.Commands.AtualizarAgendamento
{
    public class RealizarReagendamentoCommandValidator : AbstractValidator<RealizarReagendamentoCommand>
    {
        public RealizarReagendamentoCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.EspecialidadeMedicoId)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.PacienteId)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.DataHoraAtendimento)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}