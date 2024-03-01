using FitnessAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessAI.Infrastructure.Persistence.Configurations;

public class WorkoutCalendarExerciseConfiguration: IEntityTypeConfiguration<WorkoutCalendarExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutCalendarExercise> builder)
    {
        builder
            .ToTable("WorkoutCalendarExercises");
        
        builder
            .HasOne(x => x.WorkoutCalendar)
            .WithMany(x => x.WorkoutCalendarExercises)
            .HasForeignKey(x => x.WorkoutCalendarId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(x => x.UserExercise)
            .WithMany(x => x.WorkoutCalendarExercises)
            .HasForeignKey(x => x.UserExerciseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}