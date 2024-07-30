using MediatR;

namespace HealthMedScheduler.Application.Features.Medicos.Commands.RemoverMedico
{
    public class RemoverMedicoCommand : IRequest<Guid>
    {
        public Guid IdMedico { get; set; }

    }
}
