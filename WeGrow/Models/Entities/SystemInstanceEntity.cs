using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WeGrow.DAL.Entities;

namespace WeGrow.Models.Entities
{
    [AutoMap(typeof(SystemInstance), ReverseMap = true)]
    public class SystemInstanceEntity
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string User_Id { get; set; }
        public bool Is_Active { get; set; }
    }
}
