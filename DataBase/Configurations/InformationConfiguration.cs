using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestAccountingInformation.DataBase.Entities;

namespace TestAccountingInformation.DataBase.Configurations
{
    public class InformationConfiguration : IEntityTypeConfiguration<InformationEntity>
    {
        public void Configure(EntityTypeBuilder<InformationEntity> builder)
        {
            builder.ToTable("Informations");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Type)
                .HasColumnName("Type")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasData(
                    new InformationEntity { Id = 1, Type = "2 НДФЛ" },
                    new InformationEntity { Id = 2, Type = "О месте работы и стаже" },
                    new InformationEntity { Id = 3, Type = "О среднем заработке" },
                    new InformationEntity { Id = 4, Type = "Произвольного типа" }
                );
        }
    }
}
