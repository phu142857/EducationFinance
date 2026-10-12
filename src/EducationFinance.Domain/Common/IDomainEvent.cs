
namespace EducationFinance.Domain.Common;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}
