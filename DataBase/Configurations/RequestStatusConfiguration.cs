using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestAccountingInformation.Constants;
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
                new RequestStatusEntity { Id = (int)RequestStatus.Sent, Status = "Отправлен" },
                new RequestStatusEntity { Id = (int)RequestStatus.InProgress, Status = "В работе" },
                new RequestStatusEntity { Id = (int)RequestStatus.Completed, Status = "Выполнен" },
                new RequestStatusEntity { Id = (int)RequestStatus.Rejected, Status = "Отклонен" }
            );
        }
    }
}
