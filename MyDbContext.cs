using System.Reflection;
using EventHubPackage.Core;
using Microsoft.EntityFrameworkCore;

namespace WebApi;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var models = modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys());
        foreach (var relationship in models)
        {
            relationship.DeleteBehavior = DeleteBehavior.NoAction;
        }

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}

public static class DatabaseInjection
{
    public static void AddDatabase(this IServiceCollection service)
    {
        service.AddDbContext<MyDbContext>(options => { options.UseNpgsql(MyEnvironment.DbConnection); });
    }
}