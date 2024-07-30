using HealthMedScheduler.Domain.Entity;

namespace HealthMedScheduler.Domain.Interfaces
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Task<Paciente> ObterAgendamentosPorIdPaciente(Guid idPaciente);
        Task<Paciente> ObterPacientePorCPF(string cpf);
        Task<bool> VerificarAgendaLivrePaciente(Guid idPaciente, DateTime dataAtendimento);

        Task<bool> VerificarPacienteCadastrado(Guid idPaciente);
    }
}
