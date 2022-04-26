using WeGrow.Models.Entities;

namespace WeGrow.Models.Shop
{
    public class ShopModel
    {
        public List<ModuleEntity> Items { get; set; }
        public int PagesCount { get; set; } = 1;
    }
}
