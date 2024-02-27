using FitnessAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessAI.Infrastructure.Persistence.Configurations;

internal class UserExerciseConfiguration : IEntityTypeConfiguration<UserExercise>
{
    public void Configure(EntityTypeBuilder<UserExercise> builder)
    {
        builder
            .ToTable("UserExercises");
        
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserExercises)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(x => x.Exercise)
            .WithMany(x => x.UserExercises)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}