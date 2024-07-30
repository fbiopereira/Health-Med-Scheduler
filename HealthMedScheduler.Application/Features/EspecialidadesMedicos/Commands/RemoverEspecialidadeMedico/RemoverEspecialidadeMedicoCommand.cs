using MediatR;

namespace HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.RemoverEspecialidadeMedico
{
    public class RemoverEspecialidadeMedicoCommand : IRequest<Guid>
    {
        public Guid IdEspecialdiadeMedico { get; set; }

    }
}
