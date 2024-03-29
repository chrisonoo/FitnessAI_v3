﻿using FitnessAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using File = FitnessAI.Domain.Entities.File;

namespace FitnessAI.Application.Common.Interfaces;

public interface IApplicationDbContext : IDisposable
{
    DbSet<Address> Addresses { get; set; }
    DbSet<Client> Clients { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<EmployeeEvent> EmployeeEvents { get; set; }
    DbSet<SettingsPosition> SettingsPositions { get; set; }
    DbSet<Domain.Entities.Settings> Settings { get; set; }
    DbSet<Ticket> Tickets { get; set; }
    DbSet<TicketType> TicketTypes { get; set; }
    DbSet<File> Files { get; set; }
    DbSet<Announcement> Announcements { get; set; }
    DbSet<Language> Languages { get; set; }
    DbSet<TicketTypeTranslation> TicketTypeTranslations { get; set; }
    DbSet<ApplicationUser> Users { get; set; }
    DbSet<Exercise> Exercises { get; set; }
    DbSet<UserExercise> UserExercises { get; set; }
    DbSet<Workout> Workouts { get; set; }
    DbSet<WorkoutExercise> WorkoutExercises { get; set; }
    DbSet<WorkoutCalendar> WorkoutCalendars { get; set; }
    DbSet<WorkoutCalendarExercise> WorkoutCalendarExercises { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}