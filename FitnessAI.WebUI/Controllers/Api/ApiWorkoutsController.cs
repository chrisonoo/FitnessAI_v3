using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.WorkoutsCalendar.Command.DeleteSelectedWorkout;
using FitnessAI.Application.WorkoutsCalendar.Command.SetWorkoutAsDone;
using FitnessAI.Application.WorkoutsCalendar.Queries.GetWorkoutsCalendar;
using FitnessAI.Domain.Entities;
using FitnessAI.WebUI.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.WebUI.Controllers.Api;

[Route("api/[controller]/[action]")]
[ApiController]
public class ApiWorkoutsController : BaseApiController
{
    public ApiWorkoutsController(
        SignInManager<ApplicationUser> signInManager,
        IApplicationDbContext context,
        IDateTimeService dateTimeService)
        : base(signInManager, context, dateTimeService)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Workouts([FromBody] ApiCurrentUserDto currentUserDto)
    {
        if (!await IsUserAuthorized(currentUserDto)) return Unauthorized();

        var selectedDate = DateTime.Today;
        if (currentUserDto.SelectedDate != null) selectedDate = DateTime.Parse(currentUserDto.SelectedDate);
        var currentUser = await Context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserName == currentUserDto.Username);

        var result = await Mediator.Send(new GetWorkoutsCalendarQuery { UserId = currentUser!.Id, SelectedDate = selectedDate });

        var workoutCalendarExercises = result.WorkoutCalendar?.WorkoutCalendarExercises.Select(x => new ApiWorkoutCalendarExerciseDto
            {
                Id = x.Id,
                IsActive = x.IsActive,
                Title = x.UserExercise.Title,
                Category = x.UserExercise.Category,
                BeginnerLoad = x.UserExercise.BeginnerLoad,
                IntermediateLoad = x.UserExercise.IntermediateLoad,
                AdvancedLoad = x.UserExercise.AdvancedLoad
            })
            .ToList();

        return Ok(new
        {
            next_date = result.NextDate,
            previous_date = result.PreviousDate,
            selected_date = result.SelectedDate,
            workouts = result.Workouts,
            workout_calendar_id = result.WorkoutCalendar?.Id,
            workout_calendar_title = result.WorkoutCalendar?.Title,
            workout_calendar_exercises = workoutCalendarExercises
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteWorkout([FromBody] ApiCurrentUserDto currentUserDto)
    {
        if (!await IsUserAuthorized(currentUserDto)) return Unauthorized();

        var currentUser = await Context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserName == currentUserDto.Username);
        
        await Mediator.Send(new DeleteSelectedWorkoutCommand { WorkoutCalendarId = currentUserDto.WorkoutCalendarId.ToString(), UserId = currentUser.Id, WorkoutDate = currentUserDto.SelectedDate });
        
        var selectedDate = DateTime.Today;
        if (currentUserDto.SelectedDate != null) selectedDate = DateTime.Parse(currentUserDto.SelectedDate);

        var result = await Mediator.Send(new GetWorkoutsCalendarQuery { UserId = currentUser!.Id, SelectedDate = selectedDate });

        var workoutCalendarExercises = result.WorkoutCalendar?.WorkoutCalendarExercises.Select(x => new ApiWorkoutCalendarExerciseDto
            {
                Id = x.Id,
                IsActive = x.IsActive,
                Title = x.UserExercise.Title,
                Category = x.UserExercise.Category,
                BeginnerLoad = x.UserExercise.BeginnerLoad,
                IntermediateLoad = x.UserExercise.IntermediateLoad,
                AdvancedLoad = x.UserExercise.AdvancedLoad
            })
            .ToList();

        return Ok(new
        {
            next_date = result.NextDate,
            previous_date = result.PreviousDate,
            selected_date = result.SelectedDate,
            workouts = result.Workouts,
            workout_calendar_id = result.WorkoutCalendar?.Id,
            workout_calendar_title = result.WorkoutCalendar?.Title,
            workout_calendar_exercises = workoutCalendarExercises
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> AddWorkout([FromBody] ApiCurrentUserDto currentUserDto)
    {
        if (!await IsUserAuthorized(currentUserDto)) return Unauthorized();

        var currentUser = await Context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserName == currentUserDto.Username);
        
        await Mediator.Send(new SetWorkoutAsDoneCommand { WorkoutId = currentUserDto.WorkoutCalendarId.ToString(), UserId = currentUser.Id, WorkoutDate = currentUserDto.SelectedDate });
        
        var selectedDate = DateTime.Today;
        if (currentUserDto.SelectedDate != null) selectedDate = DateTime.Parse(currentUserDto.SelectedDate);

        var result = await Mediator.Send(new GetWorkoutsCalendarQuery { UserId = currentUser!.Id, SelectedDate = selectedDate });

        var workoutCalendarExercises = result.WorkoutCalendar?.WorkoutCalendarExercises.Select(x => new ApiWorkoutCalendarExerciseDto
            {
                Id = x.Id,
                IsActive = x.IsActive,
                Title = x.UserExercise.Title,
                Category = x.UserExercise.Category,
                BeginnerLoad = x.UserExercise.BeginnerLoad,
                IntermediateLoad = x.UserExercise.IntermediateLoad,
                AdvancedLoad = x.UserExercise.AdvancedLoad
            })
            .ToList();

        return Ok(new
        {
            next_date = result.NextDate,
            previous_date = result.PreviousDate,
            selected_date = result.SelectedDate,
            workouts = result.Workouts,
            workout_calendar_id = result.WorkoutCalendar?.Id,
            workout_calendar_title = result.WorkoutCalendar?.Title,
            workout_calendar_exercises = workoutCalendarExercises
        });
    }
}