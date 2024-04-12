using AloDoutor.Domain.Entity;

namespace AloDoutor.Application.Interfaces
{
    public interface IEspecialidadeRepository : IRepository<Especialidade>
    {
        Task<Especialidade> ObterMedicosPorEspecialidadeId(Guid idEspecialidade);
        Task<Especialidade> ObterPorNome(string nome);
    }
}
