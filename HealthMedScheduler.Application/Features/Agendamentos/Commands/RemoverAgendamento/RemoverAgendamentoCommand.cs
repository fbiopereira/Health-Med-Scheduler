using MediatR;

namespace HealthMedScheduler.Application.Features.Agendamentos.Commands.RemoverAgendamento
{
    public class RemoverAgendamentoCommand : IRequest<Guid>
    {
        public Guid IdAgendamento { get; set; }

    }
}
