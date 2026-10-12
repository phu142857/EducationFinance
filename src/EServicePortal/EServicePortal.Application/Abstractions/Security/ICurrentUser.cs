
namespace EServicePortal.Application.Abstractions.Security;

public interface ICurrentUser
{
    Guid? ParentId { get; }

    bool IsAuthenticated { get; }
}
