using EServicePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EServicePortal.Infrastructure.Persistence.Configurations;

public sealed class ParentConfiguration : IEntityTypeConfiguration<Parent>
{
    public void Configure(EntityTypeBuilder<Parent> builder)
    {
        builder.ToTable("Parents");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.ExternalIdentitySubject).HasColumnType("nvarchar(200)").HasMaxLength(200).IsRequired(false);
        builder.Property(x => x.FullName).HasColumnType("nvarchar(200)").HasMaxLength(200).IsRequired(false);
        builder.Property(x => x.Email).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired(false);
        builder.Property(x => x.PhoneNumber).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired(false);
        builder.Property(x => x.Address).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired(false);
        builder.Property(x => x.MailingAddress).HasColumnType("nvarchar(50)").HasMaxLength(50).IsRequired(false);
        builder.Property(x => x.RegistrationMethod).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.Status).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnType("datetime2").IsRequired(false);
        builder.HasIndex(x => x.ExternalIdentitySubject).IsUnique().HasFilter("[ExternalIdentitySubject] IS NOT NULL");
    }
}
