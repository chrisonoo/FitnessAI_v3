using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace FitnessAI.WebUI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCulture(this IServiceCollection service)
    {
        var supportedCultures = new List<CultureInfo>
        {
            new("pl"),
            new("en")
        };

        CultureInfo.DefaultThreadCurrentCulture = supportedCultures[0];
        CultureInfo.DefaultThreadCurrentUICulture = supportedCultures[0];

        service.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
    }
}