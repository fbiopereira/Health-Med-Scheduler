using MediatR;

namespace HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarAgenda
{
    public class AdicionarAgendaMedicoCommand : IRequest<Guid>
    {
        public AdicionarAgendaMedicoCommand() { }

        public AdicionarAgendaMedicoCommand(Guid medicoId, int diaSemana, string horaInicio, string horaFim)
        {
            MedicoId = medicoId;
            DiaSemana = diaSemana;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }

        public Guid MedicoId { get; set; }
        public int DiaSemana { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }
    }
}
