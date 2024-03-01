using MediatR;

namespace FitnessAI.Application.WorkoutsCalendar.Command.DeleteSelectedWorkout;

public class DeleteSelectedWorkoutCommand : IRequest
{
    public string WorkoutCalendarId { get; set; }
    public string UserId { get; set; }
    public string WorkoutDate { get; set; }
}