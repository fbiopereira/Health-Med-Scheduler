using HealthMedScheduler.Domain.Entity;

namespace HealthMedScheduler.Domain.Interfaces
{
    public interface IAgendaMedicoRepository : IRepository<AgendaMedico>
    {
        Task<AgendaMedico> ObterAgendaMedicoPorDia(Guid idMedico, int dia);
      
    }
}
