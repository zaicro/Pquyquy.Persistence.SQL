namespace Pquyquy.Persistence.SQL.Context;

public class AppUnitOfWork : UnitOfWork, IUnitOfWork
{
    public AppUnitOfWork(AppDbContext dbContext)
        : base(dbContext)
    {
    }
}
