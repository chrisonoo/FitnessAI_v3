using FitnessAI.Application.Announcements.Queries.Dtos;
using FitnessAI.Domain.Entities;

namespace FitnessAI.Application.Announcements.Extensions;

public static class AnnouncementExtensions
{
    public static AnnouncementDto ToDto(this Announcement announcement)
    {
        if (announcement == null)
            return null;

        return new AnnouncementDto
        {
            Date = announcement.Date,
            Description = announcement.Description
        };
    }
}