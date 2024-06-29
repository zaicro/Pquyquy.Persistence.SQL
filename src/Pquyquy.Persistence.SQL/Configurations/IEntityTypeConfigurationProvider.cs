namespace Pquyquy.Persistence.SQL.Configurations;

/// <summary>
/// Represents a provider for applying entity type configurations to a <see cref="ModelBuilder"/>.
/// </summary>
public interface IEntityTypeConfigurationProvider
{
    /// <summary>
    /// Applies entity type configurations to the provided <paramref name="modelBuilder"/>.
    /// </summary>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> instance to apply configurations to.</param>
    void ApplyConfiguration(ModelBuilder modelBuilder);
}