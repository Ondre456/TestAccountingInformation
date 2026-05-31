using Microsoft.VisualBasic;

namespace TestAccountingInformation.DataBase.Entities
{
    public class RequestInformation
    {
        public int RequestId { get; set; }
        public int InformationId { get; set; }

        public int Quantity { get; set; }

        public virtual RequestEntity Request { get; set; }
        public virtual InformationEntity Information { get; set; }
    }
}
