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
    public class EspecialidadeRepository : Repository<Especialidade>, IEspecialidadeRepository
    {
        public EspecialidadeRepository(MeuDbContext context) : base(context) { }

        public async Task<Especialidade> ObterMedicosPorEspecialidadeId(Guid idEspecialidade)
        {
            return await Db.Especialidades.AsNoTracking()
                 .Include(m => m.EspecialidadeMedicos)
                 .ThenInclude(e => e.Medico)
                 .FirstOrDefaultAsync(m => m.Id == idEspecialidade);
        }

        public async  Task<Especialidade> ObterPorNome(string nome)
        {
            return await Db.Especialidades.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Nome.Equals(nome));
        }
    }
}

