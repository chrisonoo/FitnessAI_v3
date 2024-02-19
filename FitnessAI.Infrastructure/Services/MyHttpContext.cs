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

    public string AppBaseUrl => $"{Current.Request.Scheme}://{Current.Request.Host}{Current.Request.PathBase}";
    public string IpAddress => Current.Connection.RemoteIpAddress.ToString();

    public ISession Session => Current.Session;
}