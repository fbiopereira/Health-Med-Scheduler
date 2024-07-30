using MediatR;

namespace HealthMedScheduler.Application.Features.Especialidades.Commands.RemoverEspecialidade
{
    public class RemoverEspecialidadeCommand : IRequest<Guid>
    {
        public Guid idEspecialidade { get; set; }

    }
}
