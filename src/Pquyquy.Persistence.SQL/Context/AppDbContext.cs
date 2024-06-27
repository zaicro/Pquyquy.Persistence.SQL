namespace Pquyquy.Persistence.SQL.Context;

public class AppDbContext : DbContext
{
    private readonly IEnumerable<IEntityTypeConfigurationProvider> _entityTypeConfigurationProviders;

    public AppDbContext(DbContextOptions<AppDbContext> options, IEnumerable<IEntityTypeConfigurationProvider> entityTypeConfigurationProviders)
        : base(options)
    {
        _entityTypeConfigurationProviders = entityTypeConfigurationProviders;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Logger.Logger.Instance.Info<AppDbContext>(LogUtilities.FormatData(OnModelCreating, $"Create Model. start"));
        base.OnModelCreating(modelBuilder);
        if (_entityTypeConfigurationProviders != null)
        {
            foreach (var configurationProvider in _entityTypeConfigurationProviders)
            {
                configurationProvider.ApplyConfiguration(modelBuilder);
            }
        }
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        Logger.Logger.Instance.Info<AppDbContext>(LogUtilities.FormatData(OnModelCreating, $"Create Model. start"));
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        Logger.Logger.Instance.Info<AppDbContext>(LogUtilities.FormatData("SaveChangesAsync", $"Save Model. start"));
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = entry.Entity.CreatedBy ?? "Unauthorized";
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.ModifiedBy = entry.Entity.ModifiedBy ?? "Unauthorized";
                    entry.Entity.ModifiedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = entry.Entity.ModifiedBy ?? "Unauthorized";
                    entry.Entity.ModifiedAt = DateTime.UtcNow;
                    entry.Property(nameof(AuditableEntity.CreatedBy)).IsModified = false;
                    entry.Property(nameof(AuditableEntity.CreatedAt)).IsModified = false;
                    break;
            }
        }
        Logger.Logger.Instance.Info<AppDbContext>(LogUtilities.FormatData("SaveChangesAsync", $"Save Model. start"));
        return base.SaveChangesAsync(cancellationToken);
    }
}
