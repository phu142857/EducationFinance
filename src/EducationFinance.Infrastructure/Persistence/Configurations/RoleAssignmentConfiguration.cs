using EducationFinance.Domain.Modules.Identity.Entities;
using EducationFinance.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationFinance.Infrastructure.Persistence.Configurations;

public sealed class RoleAssignmentConfiguration
    : IEntityTypeConfiguration<RoleAssignment>
{
    public void Configure(EntityTypeBuilder<RoleAssignment> builder)
    {
        builder.ToTable("RoleAssignments");

        builder.HasKey(x => x.AssignmentId);

        builder.Property(x => x.AssignmentId)
            .HasColumnName("assignment_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(x => x.RoleId)
            .HasColumnName("role_id")
            .IsRequired();

        builder.Property(x => x.SchoolId)
            .HasColumnName("school_id");

        builder.Property(x => x.AssignedAt)
            .HasColumnName("assigned_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.Property(x => x.ExpiresAt)
            .HasColumnName("expires_at")
            .HasColumnType("datetime2");

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<School>()
            .WithMany()
            .HasForeignKey(x => x.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
