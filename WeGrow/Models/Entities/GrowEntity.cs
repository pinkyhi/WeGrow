﻿using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WeGrow.Core.Enums;
using WeGrow.DAL.Entities;

namespace WeGrow.Models.Entities
{
    [AutoMap(typeof(Grow), ReverseMap = true)]
    public class GrowEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Timelaps { get; set; }
        public int Schedule_Id { get; set; }
        public string System_Id { get; set; }
        public GrowStatus Status { get; set; }
        public string HistoryFile { get; set; }
        public bool IsPublic { get; set; }
    }
}
