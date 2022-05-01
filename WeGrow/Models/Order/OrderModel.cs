using AutoMapper;
using WeGrow.Core.Enums;

namespace WeGrow.Models.Order
{
    [AutoMap(typeof(DAL.Entities.Order), ReverseMap = true)]
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public string Description { get; set; }

        public IEnumerable<ReceiptModel> Receipts { get; set; }
    }
}
