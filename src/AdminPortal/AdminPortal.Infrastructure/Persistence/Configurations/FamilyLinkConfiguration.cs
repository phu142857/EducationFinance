using AdminPortal.Domain.Modules.Identity.Entities;
using AdminPortal.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FamilyLinkConfiguration : IEntityTypeConfiguration<FamilyLink>
{
    public void Configure(EntityTypeBuilder<FamilyLink> builder)
    {
        builder.ToTable("FamilyLinks");

        builder.HasKey(x => x.LinkId);

        builder.Property(x => x.LinkId)
            .HasColumnName("link_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.ExternalParentId)
            .HasColumnName("external_parent_id")
            .IsRequired();

        builder.Property(x => x.StudentId)
            .HasColumnName("student_id")
            .IsRequired();

        builder.Property(x => x.RelationshipType)
            .HasColumnName("relationship_type")
            .HasConversion<string>()
            .HasColumnType("varchar(30)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.IsPrimary)
            .HasColumnName("is_primary")
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
