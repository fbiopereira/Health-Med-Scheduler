using HealthMedScheduler.Application.ViewModel;
using MediatR;

namespace HealthMedScheduler.Application.Features.Especialidades.Queries
{
    public record ObterEspecialidadeQuery : IRequest<IEnumerable<EspecialidadeViewModel>>;
    public record ObterEspecialidadePorIdQuery(Guid idEspecialidade) : IRequest<EspecialidadeViewModel>;
    public record ObterEspecialidadeMedicosPorIdQuery(Guid idEspecialidade) : IRequest<EspecialidadeViewModel>;

}
