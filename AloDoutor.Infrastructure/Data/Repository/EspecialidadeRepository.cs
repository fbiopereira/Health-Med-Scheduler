using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using AloDoutor.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AloDoutor.Infrastructure.Data.Repository
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

        public async Task<Especialidade> ObterPorNome(string nome)
        {
            return await Db.Especialidades.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Nome.Equals(nome));
        }
    }
}

