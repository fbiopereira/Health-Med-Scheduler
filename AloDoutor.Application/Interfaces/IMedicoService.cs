using AloDoutor.Domain.Entity;
using FluentValidation.Results;

namespace AloDoutor.Application.Interfaces
{
    public interface IMedicoService
    {
        Task<Medico> ObterAgendamentosPorIdMedico(Guid idMedico);
        Task<Medico> ObterEspecialidadesPorIdMedico(Guid idMedico);
        Task<IEnumerable<Medico>> ObterTodos();
        Task<Medico> ObterPorId(Guid id);
        Task<ValidationResult> Adicionar(Medico medico);
        Task<ValidationResult> Atualizar(Medico medico);
        Task<ValidationResult> Remover(Guid id);
    }
}
