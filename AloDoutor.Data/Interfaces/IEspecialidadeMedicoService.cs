using AloDoutor.Domain.Entity;
using FluentValidation.Results;

namespace AloDoutor.Domain.Interfaces
{
    public interface IEspecialidadeMedicoService
    {
        Task<ValidationResult> Adicionar(EspecialidadeMedico especialidade);
        Task<ValidationResult> Atualizar(EspecialidadeMedico especialidade);
        Task<ValidationResult> Remover(Guid id);
    }
}
