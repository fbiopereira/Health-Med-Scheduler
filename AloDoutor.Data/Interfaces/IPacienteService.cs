using AloDoutor.Domain.Entity;
using FluentValidation.Results;

namespace AloDoutor.Domain.Interfaces
{
    public interface IPacienteService
    {
        Task<ValidationResult> Adicionar(Paciente paciente);
        Task<ValidationResult> Atualizar(Paciente paciente);
        Task<ValidationResult> Remover(Guid id);
    }
}
