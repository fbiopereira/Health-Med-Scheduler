using AloDoutor.Domain.Entity;
using FluentValidation.Results;

namespace AloDoutor.Application.Interfaces
{
    public interface IEspecialidadeService
    {
        Task<ValidationResult> Adicionar(Especialidade especialidade);
        Task<ValidationResult> Atualizar(Especialidade especialidade);
        Task<ValidationResult> Remover(Guid id);
        Task<Especialidade> ObterPorId(Guid id);
        Task<List<Especialidade>> ObterTodos();
        Task<Especialidade> ObterMedicosPorEspecialidadeId(Guid idEspecialidade);

    }
}
