using FitnessAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Infrastructure.Persistence.Extensions;

internal static class ModelBuilderExtensionsLanguage
{
    public static void SeedLanguage(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>().HasData(
            new Language
            {
                Id = 1,
                Name = "Polski",
                Key = "pl"
            },
            new Language
            {
                Id = 2,
                Name = "Angielski",
                Key = "en"
            });
    }
}