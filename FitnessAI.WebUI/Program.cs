using AspNetCore.ReCaptcha;
using DataTables.AspNet.AspNetCore;
using DotNetEnv;
using FitnessAI.Application;
using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Infrastructure;
using FitnessAI.Infrastructure.SignalR.UserNotification;
using FitnessAI.WebUI.Extensions;
using FitnessAI.WebUI.Middlewares;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using NLog.Web;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddNLogWeb();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddCulture();

builder.Services.AddSession();

builder.Services.AddReCaptcha(option =>
{
    option.SiteKey = Environment.GetEnvironmentVariable("RECAPTCHA_SITE_KEY");
    option.SecretKey = Environment.GetEnvironmentVariable("RECAPTCHA_SECRET_KEY");
    option.Version = (ReCaptchaVersion)Enum.Parse(typeof(ReCaptchaVersion), builder.Configuration["ReCaptcha:Version"]);
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.RegisterDataTables();

builder.Services
    .AddControllersWithViews()
    .AddSessionStateTempDataProvider()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(x =>
    {
        x.DataAnnotationLocalizerProvider = (_, factory) => { return factory.Create(typeof(CommonResources)); };
    });

var app = builder.Build();
app.UseSession();

using (var scope = app.Services.CreateScope())
{
    app.UseRequestLocalization(
        app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

    app.UseInfrastructure(
        scope.ServiceProvider.GetRequiredService<IApplicationDbContext>(),
        app.Services.GetService<IAppSettingsService>(),
        app.Services.GetService<IEmail>(),
        app.Services.GetService<IWebHostEnvironment>());
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

var logger = app.Services.GetService<ILogger<Program>>();

if (app.Environment.IsDevelopment())
    logger.LogInformation("DEVELOPMENT MODE!!!");
else
    logger.LogInformation("PRODUCTION MODE!!!");

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.MapHub<NotificationUserHub>("/NotificationUserHub");

app.MapRazorPages();

app.Run();