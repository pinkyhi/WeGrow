using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGrow.Core.Enums;
using WeGrow.DAL.Entities;


namespace WeGrow.Models.Entities
{
    [AutoMap(typeof(Order), ReverseMap = true)]
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public string Description { get; set; }
        public string User_Id { get; set; }
    }
}
