
namespace EServicePortal.Domain.Common;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}
