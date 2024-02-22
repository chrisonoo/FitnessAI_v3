﻿using System.Reflection;
using FitnessAI.Application.Common.Events;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Domain.Entities;
using FitnessAI.Infrastructure.Encryption;
using FitnessAI.Infrastructure.Events;
using FitnessAI.Infrastructure.Identity;
using FitnessAI.Infrastructure.Invoices;
using FitnessAI.Infrastructure.Pdf;
using FitnessAI.Infrastructure.Persistence;
using FitnessAI.Infrastructure.Services;
using FitnessAI.Infrastructure.SignalR.UserNotification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rotativa.AspNetCore;

namespace FitnessAI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var encryptionService = new EncryptionService(new KeyInfo("kk3zd3HAIZjiZnDUhuU9OMASs4eljyPBZ1WbFdgC3UE=",
            "4ITbLvvo3BWGObJRFH4wDg=="));

        services.AddSingleton<IEncryptionService>(encryptionService);

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        // Move connection string to .env file
        // var connectionString = encryptionService.Decrypt(configuration.GetConnectionString("DefaultConnection"));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")!)
                .EnableSensitiveDataLogging());

        services.AddHostedService<LongRunningService>();
        services.AddSingleton<IBackgroundWorkerQueue, BackgroundWorkerQueue>();

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 8,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireNonAlphanumeric = true
                };
            })
            .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IRoleManagerService, RoleManagerService>();
        services.AddScoped<IUserRoleManagerService, UserRoleManagerService>();
        services.AddScoped<IUserManagerService, UserManagerService>();
        services.AddSingleton<IEmail, Email>();
        services.AddSingleton<IAppSettingsService, AppSettingsService>();
        services.AddHttpContextAccessor();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IHttpContext, MyHttpContext>();
        services.AddSingleton<IFileManagerService, FileManagerService>();
        services.AddHttpClient<IGymInvoices, GymInvoices>();
        services.AddScoped<IQrCodeGenerator, QrCodeGenerator>();
        services.AddScoped<IPdfFileGenerator, RotativaPdfGenerator>();
        services.AddScoped<IRandomService, RandomService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddSignalR();
        services.AddSingleton<IUserNotificationService, UserNotificationService>();
        services.AddSingleton<IUserConnectionManager, UserConnectionManager>();

        RegisterEvents(services);

        return services;
    }

    private static void RegisterEvents(IServiceCollection services)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();

        var assemblies = Assembly
            .GetExecutingAssembly()
            .GetReferencedAssemblies()
            .Select(Assembly.Load)
            .ToList();

        services
            .Scan(x => x.FromAssemblies(assemblies)
                .AddClasses(r => r.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
    }

    public static IApplicationBuilder UseInfrastructure(
        this IApplicationBuilder app,
        IApplicationDbContext context,
        IAppSettingsService appSettingsService,
        IEmail email,
        IWebHostEnvironment webHostEnvironment)
    {
        appSettingsService.Update(context).GetAwaiter().GetResult();
        email.Update(appSettingsService).GetAwaiter().GetResult();

        RotativaConfiguration.Setup(webHostEnvironment.WebRootPath);

        return app;
    }
}