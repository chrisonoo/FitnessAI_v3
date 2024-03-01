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
            .HasOne(x => x.Exercise)
            .WithMany(x => x.WorkoutCalendarExercises)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}