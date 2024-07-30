using HealthMedScheduler.Domain.Entity;

namespace HealthMedScheduler.Domain.Interfaces
{
    public interface IAgendamentoRepository : IRepository<Agendamento>
    {
        Task<IEnumerable<Agendamento>> ObterAgendamentosPorIStatus(int status);
        Task<IEnumerable<Agendamento>> ObterTodosAgendamentos();
        Task<Agendamento> ObterAgendamentoPorId(Guid id);
    }
}
