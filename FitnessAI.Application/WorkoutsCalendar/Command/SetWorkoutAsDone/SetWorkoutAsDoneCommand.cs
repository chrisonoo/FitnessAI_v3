using MediatR;

namespace FitnessAI.Application.WorkoutsCalendar.Command.SetWorkoutAsDone;

public class SetWorkoutAsDoneCommand : IRequest
{
    public string WorkoutId { get; set; }
    public string UserId { get; set; }
    public string WorkoutDate { get; set; }
}