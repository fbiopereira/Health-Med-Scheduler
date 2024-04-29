using AloDoutor.Application.ViewModel;
using MediatR;

namespace AloDoutor.Application.Features.Pacientes.Queries
{
    public record ObterPacientesQuery : IRequest<IEnumerable<PacienteViewModel>>;
    public record ObterPacientePorIdQuery(Guid idPaciente) : IRequest<PacienteViewModel>;
    public record ObterAgendamentoPacientePorIdQuery(Guid idPaciente) : IRequest<PacienteViewModel>;
    // public record ObterAgendamentoMedicoPorIdMedicoQuery(Guid idMedico) : IRequest<MedicoViewModel>;

}
