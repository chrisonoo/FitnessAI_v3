using Microsoft.AspNetCore.Http;

namespace FitnessAI.Application.Common.Interfaces;

public interface IHttpContext
{
    string AppBaseUrl { get; }
    string IpAddress { get; }
    ISession Session { get; }
}