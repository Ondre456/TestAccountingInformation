using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestAccountingInformation.DataBase.Entities;

public class RequestConfiguration : IEntityTypeConfiguration<RequestEntity>
{
    public void Configure(EntityTypeBuilder<RequestEntity> builder)
    {
        builder.ToTable("Requests");

        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.Author)
            .WithMany()
            .HasForeignKey(r => r.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Status)
            .WithMany()
            .HasForeignKey(r => r.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.RequestInformations)
            .WithOne(ri => ri.Request)
            .HasForeignKey(ri => ri.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(r => r.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .IsRequired()
            .HasDefaultValueSql("DATETIME('now')");
    }
}
