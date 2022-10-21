using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.LoggerService;
using Invoice.Repository;
using Invoice.Repository.Repositories;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Service.Contracts.ServiceManagers;
using Invoice.Service.HelperServices;
using Invoice.Service.ServiceManagers;
using Microsoft.EntityFrameworkCore;

namespace Invoice.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
    services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureHelperServices(this IServiceCollection services)
    {
        services.AddScoped<IDocumentGeneratorService, DocumentGeneratorService>();
        services.AddScoped<ISunatService, SunatService>();
    }

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<InvoiceContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
}