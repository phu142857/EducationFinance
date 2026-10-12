using EServicePortal.Domain.Modules.Chatbot.Enums;

namespace EServicePortal.Domain.Modules.Chatbot.Entities;

public sealed class ChatConversation
{
    public Guid ConversationId { get; set; }
    public Guid ParentId { get; set; }
    public ConversationStatus Status { get; set; }
    public bool ConsentToFasPrefill { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
}
