using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestAccountingInformation.DataBase.Entities;

namespace TestAccountingInformation.DataBase.Configurations
{
    public class RequestStatusConfiguration : IEntityTypeConfiguration<RequestStatusEntity>
    {
        public void Configure(EntityTypeBuilder<RequestStatusEntity> builder)
        {
            builder.ToTable("RequestStatuses");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Status)
                .HasColumnName("Status")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(
                new RequestStatusEntity { Id = 1, Status = "Отправлен" },
                new RequestStatusEntity { Id = 2, Status = "В работе" },
                new RequestStatusEntity { Id = 3, Status = "Выполнен" },
                new RequestStatusEntity { Id = 4, Status = "Отклонен" }
            );
        }
    }
}
