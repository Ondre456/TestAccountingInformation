namespace TestAccountingInformation.Models
{
    public class RequestItemViewModel
    {
        public int InformationId { get; set; }
        public string InformationType { get; set; }
        public int Quantity { get; set; } = 1;
        public bool IsSelected { get; set; }
    }

    public class RequestViewModel
    {
        public List<RequestItemViewModel> Items { get; set; } = new();
    }
}
