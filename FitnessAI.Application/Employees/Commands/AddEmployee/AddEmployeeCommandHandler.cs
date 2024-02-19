using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Dictionaries;
using FitnessAI.Domain.Entities;
using FitnessAI.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Employees.Commands.AddEmployee;

public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly IUserManagerService _userManagerService;

    public AddEmployeeCommandHandler(
        IApplicationDbContext context,
        IUserManagerService userManagerService,
        IDateTimeService dateTimeService)
    {
        _context = context;
        _userManagerService = userManagerService;
        _dateTimeService = dateTimeService;
    }

    public async Task<Unit> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userManagerService.CreateAsync(request.Email, request.Password, RolesDict.PRACOWNIK);

        var user = await _context.Users
            .Include(x => x.Employee)
            .Include(x => x.Address)
            .FirstOrDefaultAsync(x => x.Id == userId);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.RegisterDateTime = _dateTimeService.Now;
        user.EmailConfirmed = true;

        user.Employee = new Employee();
        user.Employee.UserId = userId;
        user.Employee.Salary = request.Salary;
        user.Employee.ImageUrl = request.ImageUrl;
        user.Employee.DateOfEmployment = request.DateOfEmployment;
        user.Employee.Position = (Position)request.PositionId;
        user.Employee.WebsiteRaw = request.WebsiteRaw;
        user.Employee.WebsiteUrl = request.WebsiteUrl;

        user.Address = new Address();
        user.Address.Country = request.Country;
        user.Address.City = request.City;
        user.Address.Street = request.Street;
        user.Address.ZipCode = request.ZipCode;
        user.Address.StreetNumber = request.StreetNumber;
        user.Address.UserId = userId;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}