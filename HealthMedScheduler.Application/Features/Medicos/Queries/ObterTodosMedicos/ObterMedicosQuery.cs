using HealthMedScheduler.Application.ViewModel;
using MediatR;

namespace HealthMedScheduler.Application.Features.Medicos.Queries.ObterTodosMedicos
{
    public record ObterMedicosQuery : IRequest<IEnumerable<MedicoViewModel>>;
    public record ObterMedicoPorIdQuery(Guid idMedico) : IRequest<MedicoViewModel>;
    public record ObterEspecialidadePorIdMedicoQuery(Guid idMedico) : IRequest<MedicoViewModel>;
    public record ObterAgendamentoMedicoPorIdMedicoQuery(Guid idMedico) : IRequest<MedicoViewModel>;

}
