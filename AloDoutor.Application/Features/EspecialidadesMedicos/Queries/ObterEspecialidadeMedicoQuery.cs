using AloDoutor.Application.ViewModel;
using MediatR;

namespace AloDoutor.Application.Features.EspecialidadesMedicos.Queries
{
    public record ObterEspecialidadeMedicoQuery : IRequest<IEnumerable<EspecialidadeMedicosViewModel>>;
    public record ObterMedicoEspecialidadePorIdQuery(Guid idPaciente) : IRequest<EspecialidadeMedicosViewModel>;

}
