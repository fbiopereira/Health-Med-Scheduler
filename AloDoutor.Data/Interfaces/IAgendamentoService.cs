using AloDoutor.Domain.Entity;
using FluentValidation.Results;

namespace AloDoutor.Domain.Interfaces
{
    public interface IAgendamentoService
    {
        Task<ValidationResult> Adicionar(Agendamento especialidade);
        Task<ValidationResult> Reagendar(Guid id, DateTime data);
        Task<ValidationResult> Cancelar(Guid id);
    }
}
