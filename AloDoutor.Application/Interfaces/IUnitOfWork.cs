namespace AloDoutor.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
