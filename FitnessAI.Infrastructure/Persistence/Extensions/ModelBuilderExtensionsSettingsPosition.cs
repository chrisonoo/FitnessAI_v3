using FitnessAI.Application.Dictionaries;
using FitnessAI.Domain.Entities;
using FitnessAI.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Infrastructure.Persistence.Extensions;

internal static class ModelBuilderExtensionsSettingsPosition
{
    public static void SeedSettingsPosition(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SettingsPosition>().HasData(
            new SettingsPosition
            {
                Id = 1,
                Key = SettingsDict.HOST_SMTP,
                Value = Environment.GetEnvironmentVariable("CONTACT_FORM_SMTP_HOST")!,
                Description = "Host",
                Type = SettingsType.Text,
                SettingsId = 1,
                Order = 1
            },
            new SettingsPosition
            {
                Id = 2,
                Key = SettingsDict.PORT,
                Value = Environment.GetEnvironmentVariable("CONTACT_FORM_SMTP_PORT")!,
                Description = "Port",
                Type = SettingsType.Integer,
                SettingsId = 1,
                Order = 2
            },
            new SettingsPosition
            {
                Id = 3,
                Key = SettingsDict.SENDER_EMAIL,
                Value = Environment.GetEnvironmentVariable("CONTACT_FORM_SENDER_EMAIL")!,
                Description = "Adres e-mail nadawcy",
                Type = SettingsType.Text,
                SettingsId = 1,
                Order = 3
            },
            new SettingsPosition
            {
                Id = 4,
                Key = SettingsDict.SENDER_EMAIL_PASSWORD,
                Value = Environment.GetEnvironmentVariable("CONTACT_FORM_SENDER_EMAIL_PASS")!,
                Description = "Hasło",
                Type = SettingsType.Password,
                SettingsId = 1,
                Order = 4
            },
            new SettingsPosition
            {
                Id = 5,
                Key = SettingsDict.SENDER_NAME,
                Value = Environment.GetEnvironmentVariable("CONTACT_FORM_SENDER_NAME")!,
                Description = "Nazwa nadawcy",
                Type = SettingsType.Text,
                SettingsId = 1,
                Order = 5
            },
            new SettingsPosition
            {
                Id = 6,
                Key = SettingsDict.SENDER_LOGIN,
                Value = "",
                Description = "Login nadawcy",
                Type = SettingsType.Text,
                SettingsId = 1,
                Order = 6
            },
            new SettingsPosition
            {
                Id = 7,
                Key = SettingsDict.BANNER_VISIBLE,
                Value = "True",
                Description = "Czy wyświetlać banner na stronie głównej?",
                Type = SettingsType.Boolean,
                SettingsId = 2,
                Order = 1
            },
            new SettingsPosition
            {
                Id = 8,
                Key = SettingsDict.FOOTER_COLOR,
                Value = "#dc3545",
                Description = "Folor footera strona głównej",
                Type = SettingsType.Color,
                SettingsId = 2,
                Order = 2
            },
            new SettingsPosition
            {
                Id = 9,
                Key = SettingsDict.ADMIN_EMAIL,
                Value = Environment.GetEnvironmentVariable("CONTACT_FORM_ADMIN_EMAIL")!,
                Description = "Główny adres e-mail administratora",
                Type = SettingsType.Text,
                SettingsId = 2,
                Order = 3
            });
    }
}