using Core;

namespace Data;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly TripTalkContext _context;

    public EfUnitOfWork(TripTalkContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}