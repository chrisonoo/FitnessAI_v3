using FitnessAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessAI.Infrastructure.Persistence.Configurations;

public class WorkoutCalendarConfiguration : IEntityTypeConfiguration<WorkoutCalendar>
{
    public void Configure(EntityTypeBuilder<WorkoutCalendar> builder)
    {
        builder
            .ToTable("WorkoutCalendars");
        
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.WorkoutCalendars)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}