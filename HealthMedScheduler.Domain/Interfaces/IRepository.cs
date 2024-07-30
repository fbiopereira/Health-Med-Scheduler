using HealthMedScheduler.Domain.Entity.Common;
using System.Linq.Expressions;

namespace HealthMedScheduler.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entidade
    {
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(TEntity entity);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        IUnitOfWork UnitOfWork { get; }
    }
}
