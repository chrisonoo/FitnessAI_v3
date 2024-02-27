using Microsoft.AspNetCore.Identity;

namespace FitnessAI.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime RegisterDateTime { get; set; }
    public bool IsDeleted { get; set; }

    public Address Address { get; set; }
    public Client Client { get; set; }
    public Employee Employee { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    public ICollection<EmployeeEvent> EmployeeEvents { get; set; } = new HashSet<EmployeeEvent>();
    public ICollection<UserExercise> UserExercises { get; set; } = new HashSet<UserExercise>();
    public ICollection<Workout> Workouts { get; set; } = new HashSet<Workout>();
}