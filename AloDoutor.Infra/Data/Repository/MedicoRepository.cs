using AloDoutor.Application.Interfaces;
using AloDoutor.Domain.Entity;
using AloDoutor.Infra.Data.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AloDoutor.Infrastructure.Data.Repository
{
    public class MedicoRepository : Repository<Medico>, IMedicoRepository
    {
        public MedicoRepository(MeuDbContext context) : base(context) { }

        public async Task<Medico> ObterAgendamentosPorIdMedico(Guid idMedico)
        {
            return await Db.Medicos
                 .Include(m => m.EspecialidadesMedicos)
                    .ThenInclude(e => e.Especialidade)
                 .ThenInclude(e => e.EspecialidadeMedicos)
                    .ThenInclude(a => a.Agendamentos)
                        .ThenInclude(p => p.Paciente)
                 .FirstOrDefaultAsync(m => m.Id == idMedico);
        }

        public async Task<Medico> ObterEspecialidadesPorIdMedico(Guid idMedico)
        {
            return await Db.Medicos.AsNoTracking()
                .Include(m => m.EspecialidadesMedicos)
                .ThenInclude(e => e.Especialidade)
                .FirstOrDefaultAsync(m => m.Id == idMedico);
        }

        public async Task<Medico> ObterPacientePorCPF(string cpf)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.Cpf == cpf);
        }

        public async Task<Medico> ObterPacientePorCRM(string crm)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.Crm == crm);
        }
    }
}
