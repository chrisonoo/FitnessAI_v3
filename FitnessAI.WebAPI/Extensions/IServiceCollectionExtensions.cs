using System.Globalization;
using System.Text;
using FitnessAI.WebAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FitnessAI.WebAPI.Extensions;

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

    public static void AddBearerAuthentication(this IServiceCollection service,
        IConfiguration configuration)
    {
        var bearerSecret = Encoding.ASCII.GetBytes(configuration.GetSection("Secret").Value);

        service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(bearerSecret),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
    }


    public static void AddSwaggerBearerAuthorization(this IServiceCollection service)
    {
        service.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ASP.NET 6 FitnessAI Web API 1"
            });

            swagger.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "ASP.NET 6 FitnessAI Web API 2"
            });

            swagger.ResolveConflictingActions(x => x.First());
            swagger.OperationFilter<RemoveVersionFromParameter>();
            swagger.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();

            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Bearer Authorization"
            });

            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
    }
}