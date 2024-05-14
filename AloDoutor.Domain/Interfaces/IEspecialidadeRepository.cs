using AloDoutor.Domain.Entity;

namespace AloDoutor.Domain.Interfaces
{
    public interface IEspecialidadeRepository : IRepository<Especialidade>
    {
        Task<Especialidade> ObterMedicosPorEspecialidadeId(Guid idEspecialidade);
        Task<Especialidade> ObterPorNome(string nome);
    }
}
