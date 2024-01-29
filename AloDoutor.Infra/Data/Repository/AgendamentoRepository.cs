using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using AloDoutor.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Infra.Data.Repository
{
    public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(MeuDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Agendamento>> ObterAgendamentosPorIStatus(int status)
        {
            StatusAgendamento statusDesejado = (StatusAgendamento)status;

            return await Db.Agendamentos
                .Include(m => m.Paciente)
                 .Include(e => e.EspecialidadeMedico)
                    .ThenInclude(e => e.Especialidade)
                   .ThenInclude(m => m.EspecialidadeMedicos)
                       .ThenInclude(p => p.Medico)
                .Where(a =>  (a.StatusAgendamento == statusDesejado || status == 0)).ToListAsync();
        }
    }
}
