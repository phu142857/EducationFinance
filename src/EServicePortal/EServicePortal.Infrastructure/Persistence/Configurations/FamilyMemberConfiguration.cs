using EServicePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EServicePortal.Infrastructure.Persistence.Configurations;

public sealed class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
{
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        builder.ToTable("FamilyMembers", table => table.HasCheckConstraint(
            "CK_FamilyMembers_ExactlyOneMember",
            "([ParentId] IS NOT NULL AND [StudentId] IS NULL) OR ([ParentId] IS NULL AND [StudentId] IS NOT NULL)"));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.FamilyId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.ParentId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.StudentId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.RelationshipType).HasConversion<string>().HasColumnType("nvarchar(40)").HasMaxLength(40).IsRequired();
        builder.Property(x => x.IsPrimaryContact).HasColumnType("bit").HasDefaultValue(false).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2").IsRequired();
        builder.HasIndex(x => x.FamilyId);
        builder.HasIndex(x => x.ParentId);
        builder.HasIndex(x => x.StudentId);
        builder.HasOne(x => x.Family).WithMany(x => x.Members).HasForeignKey(x => x.FamilyId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Parent).WithMany(x => x.FamilyMemberships).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Student).WithMany(x => x.FamilyMemberships).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.NoAction);
    }
}
