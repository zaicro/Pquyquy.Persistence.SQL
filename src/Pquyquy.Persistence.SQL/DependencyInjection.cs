namespace Pquyquy.Persistence.SQL;

public static class DependencyInjection
{
    public static void AddPquyquyPersistenceSQL(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(connectionString,
               builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddTransient(typeof(IUnitOfWork), typeof(AppUnitOfWork));

        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IEntityTypeConfigurationProvider))
            .AddClasses(classes => classes.AssignableTo<IEntityTypeConfigurationProvider>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }
}
