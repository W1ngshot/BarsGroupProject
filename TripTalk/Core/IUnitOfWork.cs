namespace Core;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}