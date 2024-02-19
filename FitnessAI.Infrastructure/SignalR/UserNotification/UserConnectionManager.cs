using FitnessAI.Application.Common.Interfaces;

namespace FitnessAI.Infrastructure.SignalR.UserNotification;

public class UserConnectionManager : IUserConnectionManager
{
    private static readonly string UserConnectionMapLocker = string.Empty;
    private static readonly Dictionary<string, List<string>> UserConnectionMap = new();

    public void KeepUserConnection(string userId, string connectionId)
    {
        lock (UserConnectionMapLocker)
        {
            if (!UserConnectionMap.ContainsKey(userId)) UserConnectionMap[userId] = new List<string>();

            UserConnectionMap[userId].Add(connectionId);
        }
    }

    public void RemoveUserConnection(string connectionId)
    {
        lock (UserConnectionMapLocker)
        {
            foreach (var userId in UserConnectionMap.Keys)
                if (UserConnectionMap.ContainsKey(userId))
                    if (UserConnectionMap[userId].Contains(connectionId))
                    {
                        UserConnectionMap[userId].Remove(connectionId);
                        break;
                    }
        }
    }

    public List<string> GetUserConnections(string userId)
    {
        var connections = new List<string>();

        try
        {
            lock (UserConnectionMapLocker)
            {
                connections = UserConnectionMap[userId];
            }
        }
        catch
        {
            // ignored
        }

        return connections;
    }
}