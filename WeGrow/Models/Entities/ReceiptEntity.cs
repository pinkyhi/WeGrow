using AutoMapper;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using WeGrow.DAL.Entities;

namespace WeGrow.Models.Entities
{
    [AutoMap(typeof(Receipt), ReverseMap = true)]
    public class ReceiptEntity
    {
        [Key]
        public int Order_Id { get; set; }
        [Key]
        public int Module_Id { get; set; }
        public string Cache_System_Id { get; set; }
        [Min(0)]
        public int Amount { get; set; }
        [Min(0)]
        public decimal Cache_Price { get; set; }
    }
}
