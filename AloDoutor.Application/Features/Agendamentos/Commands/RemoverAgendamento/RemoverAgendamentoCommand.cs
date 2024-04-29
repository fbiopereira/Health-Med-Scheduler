using MediatR;

namespace AloDoutor.Application.Features.Agendamentos.Commands.RemoverAgendamento
{
    public class RemoverAgendamentoCommand : IRequest<Guid>
    {
        public Guid IdAgendamento { get; set; }

    }
}
