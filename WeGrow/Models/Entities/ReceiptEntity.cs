using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WeGrow.DAL.Entities;

namespace WeGrow.Models.Entities
{
    [AutoMap(typeof(Receipt), ReverseMap = true)]
    public class ReceiptEntity
    {
        [Required]
        public int Order_Id { get; set; }
        [Required]
        public int Module_Id { get; set; }
        public string Cache_System_Id { get; set; }
        [Range(0, Double.PositiveInfinity)]
        public int Amount { get; set; }
        [Range(0, Double.PositiveInfinity)]
        public decimal Cache_Price { get; set; }
    }
}
