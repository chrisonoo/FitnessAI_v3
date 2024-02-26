using FitnessAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessAI.Infrastructure.Persistence.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder
            .ToTable("Exercises");
        
        builder
            .Property(e => e.ImageUrl)
            .IsUnicode(false);
        
        builder
            .Property(e => e.MultimediaUrl)
            .IsUnicode(false);
    }
}
