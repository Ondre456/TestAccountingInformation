using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Комментарий обязателен для заполнения")]
        [StringLength(4000, ErrorMessage = "Комментарий не может превышать 4000 символов")]
        public string ReasonComment { get; set; }
        public List<RequestItemViewModel> Items { get; set; } = new();
    }
}
