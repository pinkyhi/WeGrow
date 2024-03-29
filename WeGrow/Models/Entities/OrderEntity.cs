﻿using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WeGrow.Core.Enums;

namespace WeGrow.Models.Entities
{
    [AutoMap(typeof(DAL.Entities.Order), ReverseMap = true)]
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public string Description { get; set; }
        [Required]
        public string User_Id { get; set; }
    }
}
