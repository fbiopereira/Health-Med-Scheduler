using MediatR;

namespace HealthMedScheduler.Application.Features.Pacientes.Commands.RemoverPaciente
{
    public class RemoverPacienteCommand : IRequest<Guid>
    {
        public Guid IdPaciente { get; set; }

    }
}
