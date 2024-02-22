using FitnessAI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FitnessAI.Infrastructure.Services;

public class MyHttpContext : IHttpContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MyHttpContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Current => _httpContextAccessor.HttpContext;

    public ISession Session => Current.Session;
}