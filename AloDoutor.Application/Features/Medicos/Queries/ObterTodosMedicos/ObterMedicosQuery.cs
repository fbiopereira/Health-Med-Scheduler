using AloDoutor.Application.ViewModel;
using MediatR;

namespace AloDoutor.Application.Features.Medicos.Queries.ObterTodosMedicos
{
    public record ObterPacinetesQuery : IRequest<IEnumerable<MedicoViewModel>>;
    public record ObterMedicoPorIdQuery(Guid idMedico) : IRequest<MedicoViewModel>;
    public record ObterEspecialidadePorIdMedicoQuery(Guid idMedico) : IRequest<MedicoViewModel>;
    public record ObterAgendamentoMedicoPorIdMedicoQuery(Guid idMedico) : IRequest<MedicoViewModel>;

}
