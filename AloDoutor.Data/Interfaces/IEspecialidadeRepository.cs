using AloDoutor.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Domain.Interfaces
{
    public interface IEspecialidadeRepository : IRepository<Especialidade>
    {
        Task<Especialidade> ObterMedicosPorEspecialidadeId(Guid idEspecialidade);
        Task<Especialidade> ObterPorNome(string nome);
    }
}
