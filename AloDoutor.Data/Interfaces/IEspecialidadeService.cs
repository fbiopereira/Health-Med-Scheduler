using AloDoutor.Domain.Entity;
using FluentValidation.Results;

namespace AloDoutor.Domain.Interfaces
{
    public interface IEspecialidadeService
    {
        Task<ValidationResult> Adicionar(Especialidade especialidade);
        Task<ValidationResult> Atualizar(Especialidade especialidade);
        Task<ValidationResult> Remover(Guid id);
    }
}
