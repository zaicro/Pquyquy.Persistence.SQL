namespace Pquyquy.Persistence.SQL.Context;

//add-migration Init -p Pquyquy.Persistence.SQL -c AppDbContext -o Migrations -s Pquyquy.Persistence.SQL
//update-database -p Pquyquy.Persistence.SQL -s Pquyquy.Persistence.SQL
//remove-migration -p Pquyquy.Persistence.SQL -s Pquyquy.Persistence.SQL
public class AppDbContextFactory :
    IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var configurationProviders = new List<IEntityTypeConfigurationProvider>();
        optionBuilder.UseSqlServer("Server=DESKTOP-DKCA35A;Initial Catalog=xxxxxx;Integrated Security=true;TrustServerCertificate=True;");
        return new AppDbContext(optionBuilder.Options, configurationProviders);
    }
}
