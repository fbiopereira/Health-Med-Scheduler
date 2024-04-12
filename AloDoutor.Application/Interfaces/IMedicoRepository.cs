using AloDoutor.Domain.Entity;

namespace AloDoutor.Application.Interfaces
{
    public interface IMedicoRepository : IRepository<Medico>
    {
        Task<Medico> ObterAgendamentosPorIdMedico(Guid idMedico);
        Task<Medico> ObterEspecialidadesPorIdMedico(Guid idMedico);
        Task<Medico> ObterPacientePorCPF(string cpf);
        Task<Medico> ObterPacientePorCRM(string crm);
    }
}
