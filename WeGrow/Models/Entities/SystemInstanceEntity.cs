using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGrow.DAL.Entities;

namespace WeGrow.Models.Entities
{
    [AutoMap(typeof(SystemInstance), ReverseMap = true)]
    public class SystemInstanceEntity
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string User_Id { get; set; }
        public bool Is_Active { get; set; }
    }
}
