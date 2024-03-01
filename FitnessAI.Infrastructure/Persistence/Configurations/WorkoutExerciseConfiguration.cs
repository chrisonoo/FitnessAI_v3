using FitnessAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessAI.Infrastructure.Persistence.Configurations;

public class WorkoutExerciseConfiguration: IEntityTypeConfiguration<WorkoutExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
    {
        builder
            .ToTable("WorkoutExercises");
        
        builder
            .HasOne(x => x.Workout)
            .WithMany(x => x.WorkoutExercises)
            .HasForeignKey(x => x.WorkoutId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(x => x.UserExercise)
            .WithMany(x => x.WorkoutExercises)
            .HasForeignKey(x => x.UserExerciseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}