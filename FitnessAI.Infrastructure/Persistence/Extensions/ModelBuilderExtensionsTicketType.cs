using FitnessAI.Domain.Entities;
using FitnessAI.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Infrastructure.Persistence.Extensions;

internal static class ModelBuilderExtensionsTicketType
{
    public static void SeedTicketType(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TicketType>().HasData(
            new TicketType
            {
                Id = 1,
                Price = 10,
                TicketTypeEnum = TicketTypeEnum.Single
            },
            new TicketType
            {
                Id = 2,
                Price = 25,
                TicketTypeEnum = TicketTypeEnum.Weekly
            },
            new TicketType
            {
                Id = 3,
                Price = 100,
                TicketTypeEnum = TicketTypeEnum.Monthly
            },
            new TicketType
            {
                Id = 4,
                Price = 1000,
                TicketTypeEnum = TicketTypeEnum.Annual
            }
        );
    }
}