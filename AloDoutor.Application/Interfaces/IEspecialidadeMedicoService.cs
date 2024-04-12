using AloDoutor.Domain.Entity;
using FluentValidation.Results;

namespace AloDoutor.Application.Interfaces
{
    public interface IEspecialidadeMedicoService
    {
        Task<ValidationResult> Adicionar(EspecialidadeMedico especialidade);
        Task<ValidationResult> Atualizar(EspecialidadeMedico especialidade);
        Task<ValidationResult> Remover(Guid id);
        Task<EspecialidadeMedico> ObterPorId(Guid id);
        Task<List<EspecialidadeMedico>> ObterTodos();
    }
}
