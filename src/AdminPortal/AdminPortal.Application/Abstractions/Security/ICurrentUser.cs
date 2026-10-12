
namespace AdminPortal.Application.Abstractions.Security;

public interface ICurrentUser
{
    Guid? StaffUserId { get; }

    bool IsAuthenticated { get; }
}
