namespace AloDoutor.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
