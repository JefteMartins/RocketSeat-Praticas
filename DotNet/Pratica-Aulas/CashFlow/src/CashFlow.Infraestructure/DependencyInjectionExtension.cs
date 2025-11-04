using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infraestructure.DataAccess;
using CashFlow.Infraestructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infraestructure;

public static class DependencyInjectionExtension
{
    public static void ConfigureInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
    }

    /// <summary>
    /// Registers repository and unit-of-work services in the provided DI service collection.
    /// </summary>
    /// <remarks>
    /// Adds the following scoped registrations:
    /// - IUnitOfWork -> UnitOfWork
    /// - IExpensesReadOnlyRepository -> ExpensesRepository
    /// - IExpensesWriteOnlyRepository -> ExpensesRepository
    /// - IExpensesUpdateOnlyRepository -> ExpensesRepository
    /// </remarks>
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();
    }

    /// <summary>
    /// Registers CashFlowDbContext in the DI container configured to use MySQL (server version 8.0.43)
    /// with the connection string named "Connection" from the provided configuration.
    /// </summary>
    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("Connection");
        var version = new Version(8, 0, 43);
        var serverVersion = new MySqlServerVersion(version);

        services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));

    }
}