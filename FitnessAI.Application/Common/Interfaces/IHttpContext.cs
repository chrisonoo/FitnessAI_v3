using Microsoft.AspNetCore.Http;

namespace FitnessAI.Application.Common.Interfaces;

public interface IHttpContext
{
    ISession Session { get; }
}