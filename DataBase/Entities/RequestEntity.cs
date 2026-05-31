using TestAccountingInformation.DataBase.Entityes;

namespace TestAccountingInformation.DataBase.Entities
{
    public class RequestEntity
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public UserEntity Author { get; set; }
        public string? ExecutorId { get; set; }
        public UserEntity? Executor { get; set; }
        public virtual ICollection<RequestInformation> RequestInformations { get; set; } = new List<RequestInformation>();
        public int StatusId { get; set; }
        public RequestStatusEntity Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
