using Invoice.Contracts.Repositories;
using Invoice.Repository;
using Invoice.Repository.Repositories;
using Invoice.Service.BusinessServices;
using Invoice.Service.Contracts.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Service.HelperServices;
using Microsoft.EntityFrameworkCore;

namespace Invoice.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IIssuerService, IssuerService>();
        services.AddScoped<ISerializeXmlService, SerializeXmlService>();
        services.AddScoped<ISignerService, SignerService>();
        services.AddScoped<IZipperService, ZipperService>();
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IIssuerRepository, IssuerRepository>();
    }

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<InvoiceContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
}