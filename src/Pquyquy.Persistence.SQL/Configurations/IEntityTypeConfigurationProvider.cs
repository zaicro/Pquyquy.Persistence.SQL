namespace Pquyquy.Persistence.SQL.Configurations;

public interface IEntityTypeConfigurationProvider
{
    void ApplyConfiguration(ModelBuilder modelBuilder);
}
