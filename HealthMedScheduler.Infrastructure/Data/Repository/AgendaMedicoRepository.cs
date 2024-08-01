using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using HealthMedScheduler.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthMedScheduler.Infrastructure.Data.Repository
{
    public class AgendaMedicoRepository : Repository<AgendaMedico>, IAgendaMedicoRepository
    {
        public AgendaMedicoRepository(MeuDbContext context) : base(context) { }

        public async Task<AgendaMedico> ObterAgendaMedicoPorDia(Guid idMedico, int dia)
        {
            var result = await DbSet.FirstOrDefaultAsync(a => a.MedicoId == idMedico && a.DiaSemana == (DayOfWeek) dia);
            return result;
        }
    }
}
