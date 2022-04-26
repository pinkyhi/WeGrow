namespace WeGrow.Models.Shop
{
    public class ModulesShopFilterModel
    {
        public IEnumerable<string> Types { get; set; }
        public IEnumerable<string> Subjects { get; set; }
        public string SortingType { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public bool? IsInStock { get; set; }
    }
}
