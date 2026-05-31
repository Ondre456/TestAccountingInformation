using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestAccountingInformation.DataBase.Entities;

namespace TestAccountingInformation.DataBase.Configurations
{
    public class RequestInformationConfiguration : IEntityTypeConfiguration<RequestInformation>
    {
        public void Configure(EntityTypeBuilder<RequestInformation> builder)
        {
            builder.ToTable("RequestInformations");

            builder.HasKey(ri => new { ri.RequestId, ri.InformationId });

            builder.Property(ri => ri.Quantity)
                .HasDefaultValue(1);

            builder.HasOne(ri => ri.Request)
                .WithMany(r => r.RequestInformations)
                .HasForeignKey(ri => ri.RequestId);

            builder.HasOne(ri => ri.Information)
                .WithMany()
                .HasForeignKey(ri => ri.InformationId);
        }
    }
}
