using Microsoft.AspNetCore.Identity;

namespace TestAccountingInformation.DataBase.Entityes
{
    public class UserEntity : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
