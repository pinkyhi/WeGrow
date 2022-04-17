﻿using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WeGrow.DAL.Entities;

namespace WeGrow.Models.Entities
{
    [AutoMap(typeof(ModuleInstance), ReverseMap = true)]
    public class ModuleInstanceEntity
    {
        [Key]
        public string Id { get; set; }
        public string System_Id { get; set; }
        public int Module_Id { get; set; }
        public DateTime LastResponse { get; set; }
    }
}
