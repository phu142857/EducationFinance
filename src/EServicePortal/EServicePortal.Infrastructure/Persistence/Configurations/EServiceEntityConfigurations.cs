using EServicePortal.Domain.Modules.Audit.Entities;
using EServicePortal.Domain.Modules.Chatbot.Entities;
using EServicePortal.Domain.Modules.EducationAccounts.Entities;
using EServicePortal.Domain.Modules.Family.Entities;
using EServicePortal.Domain.Modules.FinancialAssistance.Entities;
using EServicePortal.Domain.Modules.Identity.Entities;
using EServicePortal.Domain.Modules.Notifications.Entities;
using EServicePortal.Domain.Modules.Payments.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EServicePortal.Infrastructure.Persistence.Configurations;

internal sealed class ParentAccountConfiguration : IEntityTypeConfiguration<ParentAccount>
{
    public void Configure(EntityTypeBuilder<ParentAccount> builder)
    {
        builder.ToTable("ParentAccounts");
        builder.HasKey(x => x.ParentId);
        builder.Property(x => x.SingpassUinFin).HasMaxLength(20).IsUnicode(false);
        builder.HasIndex(x => x.SingpassUinFin).IsUnique().HasFilter("[singpass_uin_fin] IS NOT NULL");
        builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(254).IsUnicode(false);
        builder.Property(x => x.PhoneNumber).HasMaxLength(30).IsUnicode(false);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsUnicode(false).IsRequired();
    }
}

internal sealed class AssistedRegistrationConfiguration : IEntityTypeConfiguration<AssistedRegistration>
{
    public void Configure(EntityTypeBuilder<AssistedRegistration> builder)
    {
        builder.ToTable("AssistedRegistrations");
        builder.HasKey(x => x.RegistrationId);
        builder.Property(x => x.RegistrationCodeHash).HasMaxLength(128).IsUnicode(false).IsRequired();
        builder.Property(x => x.ParentIdentificationHash).HasMaxLength(128).IsUnicode(false).IsRequired();
        builder.Property(x => x.ChildBirthCertificateHash).HasMaxLength(128).IsUnicode(false).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsUnicode(false).IsRequired();
        builder.HasIndex(x => x.RegistrationCodeHash).IsUnique();
    }
}

internal sealed class ParentProfileConfiguration : IEntityTypeConfiguration<ParentProfile>
{
    public void Configure(EntityTypeBuilder<ParentProfile> builder)
    {
        builder.ToTable("ParentProfiles");
        builder.HasKey(x => x.ParentId);
        builder.Property(x => x.ResidentialAddress).HasMaxLength(500);
        builder.Property(x => x.MailingAddress).HasMaxLength(500);
        builder.HasOne<ParentAccount>().WithOne().HasForeignKey<ParentProfile>(x => x.ParentId).OnDelete(DeleteBehavior.Cascade);
    }
}

internal sealed class ChildProfileConfiguration : IEntityTypeConfiguration<ChildProfile>
{
    public void Configure(EntityTypeBuilder<ChildProfile> builder)
    {
        builder.ToTable("ChildProfiles");
        builder.HasKey(x => x.ChildId);
        builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
        builder.Property(x => x.SchoolName).HasMaxLength(200).IsRequired();
        builder.HasIndex(x => x.ExternalStudentId).IsUnique();
    }
}

internal sealed class FamilyLinkConfiguration : IEntityTypeConfiguration<FamilyLink>
{
    public void Configure(EntityTypeBuilder<FamilyLink> builder)
    {
        builder.ToTable("FamilyLinks");
        builder.HasKey(x => x.LinkId);
        builder.Property(x => x.RelationshipType).HasConversion<string>().HasMaxLength(30).IsUnicode(false).IsRequired();
        builder.HasIndex(x => new { x.ParentId, x.ChildId }).IsUnique();
        builder.HasOne<ParentAccount>().WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<ChildProfile>().WithMany().HasForeignKey(x => x.ChildId).OnDelete(DeleteBehavior.Restrict);
    }
}

internal sealed class EducationAccountSummaryConfiguration : IEntityTypeConfiguration<EducationAccountSummary>
{
    public void Configure(EntityTypeBuilder<EducationAccountSummary> builder)
    {
        builder.ToTable("EducationAccountSummaries");
        builder.HasKey(x => x.AccountId);
        builder.Property(x => x.AccountNumberMasked).HasMaxLength(50).IsUnicode(false).IsRequired();
        builder.Property(x => x.Balance).HasPrecision(18, 2);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsUnicode(false).IsRequired();
        builder.HasOne<ChildProfile>().WithMany().HasForeignKey(x => x.ChildId).OnDelete(DeleteBehavior.Restrict);
    }
}

internal sealed class AccountTransactionViewConfiguration : IEntityTypeConfiguration<AccountTransactionView>
{
    public void Configure(EntityTypeBuilder<AccountTransactionView> builder)
    {
        builder.ToTable("AccountTransactionViews");
        builder.HasKey(x => x.TransactionId);
        builder.Property(x => x.TransactionType).HasConversion<string>().HasMaxLength(40).IsUnicode(false).IsRequired();
        builder.Property(x => x.Amount).HasPrecision(18, 2);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.HasOne<EducationAccountSummary>().WithMany().HasForeignKey(x => x.AccountId).OnDelete(DeleteBehavior.Cascade);
    }
}

internal sealed class ChargeSummaryConfiguration : IEntityTypeConfiguration<ChargeSummary>
{
    public void Configure(EntityTypeBuilder<ChargeSummary> builder)
    {
        builder.ToTable("ChargeSummaries");
        builder.HasKey(x => x.ChargeId);
        builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
        builder.Property(x => x.Amount).HasPrecision(18, 2);
        builder.Property(x => x.OutstandingAmount).HasPrecision(18, 2);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsUnicode(false).IsRequired();
        builder.HasIndex(x => x.ExternalAssessmentId).IsUnique();
        builder.HasOne<EducationAccountSummary>().WithMany().HasForeignKey(x => x.AccountId).OnDelete(DeleteBehavior.Cascade);
    }
}

internal sealed class FasApplicationConfiguration : IEntityTypeConfiguration<FasApplication>
{
    public void Configure(EntityTypeBuilder<FasApplication> builder)
    {
        builder.ToTable("FasApplications");
        builder.HasKey(x => x.ApplicationId);
        builder.Property(x => x.ReferenceNumber).HasMaxLength(50).IsUnicode(false);
        builder.HasIndex(x => x.ReferenceNumber).IsUnique().HasFilter("[reference_number] IS NOT NULL");
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(40).IsUnicode(false).IsRequired();
        builder.Property(x => x.HouseholdIncome).HasPrecision(18, 2);
        builder.HasOne<ParentAccount>().WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
    }
}

internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(x => x.PaymentId);
        builder.Property(x => x.PaymentMethod).HasMaxLength(30).IsUnicode(false).IsRequired();
        builder.Property(x => x.Amount).HasPrecision(18, 2);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsUnicode(false).IsRequired();
        builder.Property(x => x.IdempotencyKey).HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(x => x.ProviderReference).HasMaxLength(100).IsUnicode(false);
        builder.HasIndex(x => x.IdempotencyKey).IsUnique();
        builder.HasOne<ParentAccount>().WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<ChargeSummary>().WithMany().HasForeignKey(x => x.ChargeId).OnDelete(DeleteBehavior.Restrict);
    }
}

internal sealed class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> builder)
    {
        builder.ToTable("Receipts");
        builder.HasKey(x => x.ReceiptId);
        builder.Property(x => x.ReferenceNumber).HasMaxLength(50).IsUnicode(false).IsRequired();
        builder.Property(x => x.Amount).HasPrecision(18, 2);
        builder.HasIndex(x => x.ReferenceNumber).IsUnique();
        builder.HasIndex(x => x.PaymentId).IsUnique();
        builder.HasOne<Payment>().WithOne().HasForeignKey<Receipt>(x => x.PaymentId).OnDelete(DeleteBehavior.Restrict);
    }
}

internal sealed class ChatConversationConfiguration : IEntityTypeConfiguration<ChatConversation>
{
    public void Configure(EntityTypeBuilder<ChatConversation> builder)
    {
        builder.ToTable("ChatConversations");
        builder.HasKey(x => x.ConversationId);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsUnicode(false).IsRequired();
        builder.HasOne<ParentAccount>().WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
    }
}

internal sealed class ParentNotificationConfiguration : IEntityTypeConfiguration<ParentNotification>
{
    public void Configure(EntityTypeBuilder<ParentNotification> builder)
    {
        builder.ToTable("ParentNotifications");
        builder.HasKey(x => x.NotificationId);
        builder.Property(x => x.NotificationType).HasConversion<string>().HasMaxLength(50).IsUnicode(false).IsRequired();
        builder.Property(x => x.Subject).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsUnicode(false).IsRequired();
        builder.HasOne<ParentAccount>().WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
    }
}

internal sealed class AuditEventConfiguration : IEntityTypeConfiguration<AuditEvent>
{
    public void Configure(EntityTypeBuilder<AuditEvent> builder)
    {
        builder.ToTable("AuditEvents");
        builder.HasKey(x => x.EventId);
        builder.Property(x => x.ActionType).HasConversion<string>().HasMaxLength(50).IsUnicode(false).IsRequired();
        builder.Property(x => x.TargetEntity).HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(x => x.OldValues).HasColumnType("nvarchar(max)");
        builder.Property(x => x.NewValues).HasColumnType("nvarchar(max)");
        builder.Property(x => x.IpAddress).HasMaxLength(45).IsUnicode(false);
        builder.HasOne<ParentAccount>().WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
    }
}
