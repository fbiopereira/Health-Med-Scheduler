using HealthMedScheduler.Application.ViewModel;
using MediatR;

namespace HealthMedScheduler.Application.Features.Pacientes.Queries
{
    public record ObterPacientesQuery : IRequest<IEnumerable<PacienteViewModel>>;
    public record ObterPacientePorIdQuery(Guid idPaciente) : IRequest<PacienteViewModel>;
    public record ObterAgendamentoPacientePorIdQuery(Guid idPaciente) : IRequest<PacienteViewModel>;

}
