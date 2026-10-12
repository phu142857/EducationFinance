
namespace AdminPortal.Domain.Common;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}
