using MediatR;

namespace HealthMedScheduler.Application.Features.Agendamentos.Commands.AtualizarAgendamento
{
    public class RealizarReagendamentoCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid PacienteId { get; set; }
        public Guid EspecialidadeMedicoId { get; set; }
        public DateTime DataHoraAtendimento { get; set; }
    }
}
