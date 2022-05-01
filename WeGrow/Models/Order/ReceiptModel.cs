using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WeGrow.Models.Entities;

namespace WeGrow.Models.Order
{
    [AutoMap(typeof(DAL.Entities.Receipt), ReverseMap = true)]
    public class ReceiptModel
    {
        public int Order_Id { get; set; }
        public int Module_Id { get; set; }
        public string Cache_System_Id { get; set; }
        [Range(0, Double.PositiveInfinity)]
        public int Amount { get; set; }
        [Range(0, Double.PositiveInfinity)]
        public decimal Cache_Price { get; set; }

        public ModuleEntity Module { get; set; }
    }
}
