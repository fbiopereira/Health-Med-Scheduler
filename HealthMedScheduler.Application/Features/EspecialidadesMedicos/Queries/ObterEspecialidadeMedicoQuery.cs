using HealthMedScheduler.Application.ViewModel;
using MediatR;

namespace HealthMedScheduler.Application.Features.EspecialidadesMedicos.Queries
{
    public record ObterEspecialidadeMedicoQuery : IRequest<IEnumerable<EspecialidadeMedicosViewModel>>;
    public record ObterMedicoEspecialidadePorIdQuery(Guid idPaciente) : IRequest<EspecialidadeMedicosViewModel>;

}
