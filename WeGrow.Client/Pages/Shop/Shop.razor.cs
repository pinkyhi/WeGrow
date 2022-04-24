using Microsoft.AspNetCore.Components;
using WeGrow.Core.Resources;
using WeGrow.Models.Entities;
using WeGrow.Models.Shop;

namespace WeGrow.Client.Pages.Shop
{
    partial class Shop
    {
        public List<ModuleEntity> ItemsList = new();

        [Parameter]
        [SupplyParameterFromQuery(Name = "page")]
        public int? CurrentPage { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "search")]
        public string Search { get; set; }

        public ModulesShopFilter FilterModel { get; set; } = new();

        public string ApiUrl { get; set; }

        [Inject] private IConfiguration Configuration { get; set; }

        protected override void OnInitialized()
        {
            ApiUrl = Configuration["apiUrl"] + ApiRoutes.ShopModules;
        }

        protected async Task OnFilterApplied(ModulesShopFilter filterModel)
        {
            await Task.Delay(2000);
        }
    }
}
