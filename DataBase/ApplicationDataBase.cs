using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestAccountingInformation.DataBase.Configurations;
using TestAccountingInformation.DataBase.Entityes;

namespace TestAccountingInformation.DataBase
{
    public class ApplicationDataBase : IdentityDbContext<UserEntity>
    {
        public ApplicationDataBase(DbContextOptions<ApplicationDataBase> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration());
        }
    }
}
