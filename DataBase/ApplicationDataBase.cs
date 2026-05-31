using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TestAccountingInformation.DataBase.Configurations;
using TestAccountingInformation.DataBase.Entities;
using TestAccountingInformation.DataBase.Entityes;

namespace TestAccountingInformation.DataBase
{
    public class ApplicationDataBase : IdentityDbContext<UserEntity>
    {
        public ApplicationDataBase(DbContextOptions<ApplicationDataBase> options)
            : base(options) { }

        public DbSet<RequestEntity> Requests { get; set; }
        public DbSet<InformationEntity> Informations { get; set; }
        public DbSet<RequestInformation> RequestInformations { get; set; }
        public DbSet<RequestStatusEntity> RequestStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration());
            modelBuilder.ApplyConfiguration(new InformationConfiguration());
            modelBuilder.ApplyConfiguration(new RequestConfiguration());
            modelBuilder.ApplyConfiguration(new RequestInformationConfiguration());
            modelBuilder.ApplyConfiguration(new RequestStatusConfiguration());
        }
    }
}
