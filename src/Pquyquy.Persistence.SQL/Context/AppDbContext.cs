namespace Pquyquy.Persistence.SQL.Context;

public class AppDbContext : DbContext
{
    private readonly IEnumerable<IEntityTypeConfigurationProvider> _entityTypeConfigurationProviders;
    private const string UnauthorizedUser = "Unauthorized";

    public AppDbContext(DbContextOptions<AppDbContext> options, IEnumerable<IEntityTypeConfigurationProvider> entityTypeConfigurationProviders)
            : base(options)
    {
        _entityTypeConfigurationProviders = entityTypeConfigurationProviders;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        if (_entityTypeConfigurationProviders != null)
        {
            Logger.Instance.Debug<AppDbContext>(nameof(OnModelCreating), "entityTypeConfigurationProviders: Start");
            foreach (var configurationProvider in _entityTypeConfigurationProviders)
            {
                configurationProvider.ApplyConfiguration(modelBuilder);
            }
            Logger.Instance.Debug<AppDbContext>(nameof(OnModelCreating), "entityTypeConfigurationProviders: End");
        }
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        Logger.Instance.Debug<AppDbContext>(nameof(SaveChangesAsync), "SaveChangesAsync(AuditableEntity): Start");
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = entry.Entity.CreatedBy ?? UnauthorizedUser;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.ModifiedBy = entry.Entity.ModifiedBy ?? UnauthorizedUser;
                    entry.Entity.ModifiedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = entry.Entity.ModifiedBy ?? UnauthorizedUser;
                    entry.Entity.ModifiedAt = DateTime.UtcNow;
                    entry.Property(nameof(AuditableEntity.CreatedBy)).IsModified = false;
                    entry.Property(nameof(AuditableEntity.CreatedAt)).IsModified = false;
                    break;
            }
        }
        Logger.Instance.Debug<AppDbContext>(nameof(SaveChangesAsync), "SaveChangesAsync(AuditableEntity): End");

        return base.SaveChangesAsync(cancellationToken);
    }
}
