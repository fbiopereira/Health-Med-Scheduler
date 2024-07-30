using MediatR;

namespace HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialidadeMedico
{
    public class AdicionarEspecialidadeMedicoCommand : IRequest<Guid>
    {
        public Guid EspecialidadeId { get; set; }
        public Guid MedicoId { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
