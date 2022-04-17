using WeGrow.Core.Enums;

namespace WeGrow.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public string Description { get; set; }
        public string User_Id { get; set; }

        public IEnumerable<Receipt> Receipts { get; set; }
    }
}
