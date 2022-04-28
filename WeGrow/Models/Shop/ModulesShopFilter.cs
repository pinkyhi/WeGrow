using AutoMapper;
using WeGrow.Core.Enums;

namespace WeGrow.Models.Shop
{
    [AutoMap(typeof(ModulesShopFilterModel), ReverseMap = true)]
    public class ModulesShopFilter
    {
        public IEnumerable<ModuleType> Types { get; set; }
        public IEnumerable<ModuleSubject> Subjects { get; set; }
        public SortingType? SortingType { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public bool? IsInStock { get; set; }
    }
}
