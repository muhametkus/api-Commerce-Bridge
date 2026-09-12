using CommerceBridge.Application.Common.Interfaces;
using CommerceBridge.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CommerceBridge.Infrastructure.Integrations.Payments.Sipay;
using Microsoft.Extensions.Options;

namespace CommerceBridge.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "DefaultConnection connection string was not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IApplicationDbContext>(
            provider =>
                provider.GetRequiredService<ApplicationDbContext>());
        
        services.Configure<SipayOptions>(
            configuration.GetSection(
                SipayOptions.SectionName));

        services.AddHttpClient<SipayClient>(
            (serviceProvider, client) =>
            {
                var options = serviceProvider
                    .GetRequiredService<IOptions<SipayOptions>>()
                    .Value;

                client.BaseAddress =
                    new Uri(options.BaseUrl);

                client.Timeout =
                    TimeSpan.FromSeconds(30);
            });

        services.AddScoped<
            IPaymentGateway,
            SipayPaymentGateway>();

        return services;
    }
}