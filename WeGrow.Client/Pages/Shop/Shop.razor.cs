using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using WeGrow.Core.Helpers;
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
        public int CurrentPage { get; set; } = 0;

        [Parameter]
        [SupplyParameterFromQuery(Name = "search")]
        public string Search { get; set; }

        public ModulesShopFilter FilterModel { get; set; } = new();

        public string ApiUrl { get; set; }

        private bool isLoading = false;

        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }

        protected override void OnInitialized()
        {
            ApiUrl = Configuration["apiUrl"] + ApiRoutes.ShopModules;
        }

        protected async Task OnFilterApplied(ModulesShopFilter filterModel)
        {
            
            var queryParams = new Dictionary<string, string>();

            queryParams.Add("page", CurrentPage.ToString());

            if (!string.IsNullOrWhiteSpace(Search))
            {
                queryParams.Add("search", Search);
            }
            foreach(var param in QueryMapHelper.GetDictionaryFromModel(filterModel))
            {
                queryParams.Add(param.Key, param.Value);
            }

            var uri = new Uri(QueryHelpers.AddQueryString(ApiUrl, queryParams));
            await JsRuntime.InvokeVoidAsync("ChangeQueryString", uri.Query);

            isLoading = true;

            var result = await HttpClient.GetAsync(uri);

            if (result.IsSuccessStatusCode)
            {
                ItemsList = await result.Content.ReadFromJsonAsync<List<ModuleEntity>>();
                isLoading = false;
            }
            else
            {
                isLoading = false;
                throw new Exception("Fetch data error");
            }
        }
    }
}
