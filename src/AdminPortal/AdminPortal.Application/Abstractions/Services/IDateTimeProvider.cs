
namespace AdminPortal.Application.Abstractions.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }

    DateOnly TodayUtc { get; }
}
