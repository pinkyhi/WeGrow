using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using System.Web;
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
        public int CurrentPage { get; set; } = 1;

        [Parameter]
        [SupplyParameterFromQuery(Name = "search")]
        public string Search { get; set; }

        public int PagesCount { get; set; } = 1;

        public ModulesShopFilterModel FilterModel { get; set; } = new();

        public string ApiUrl { get; set; }

        private bool isLoading = true;

        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }
        [Inject] private IHttpContextAccessor Accessor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ApiUrl = Configuration["apiUrl"] + ApiRoutes.ShopModules;
            string currentLocation = "";
            try
            {
                currentLocation = await JsRuntime.InvokeAsync<string>("GetWindowLocation");
            }
            catch
            {
                currentLocation = Accessor.HttpContext.Request.GetEncodedUrl();
            }
            FilterModel = QueryMapHelper.GetModelFromQueryUrl<ModulesShopFilterModel>(currentLocation);

            if (ItemsList.Count == 0)
            {
                var requestUrl = QueryHelpers.AddQueryString(ApiUrl, QueryMapHelper.NameValuesToDictionary(HttpUtility.ParseQueryString(new Uri(currentLocation).Query)));
                var uri = new Uri(requestUrl);
                isLoading = true;

                var result = await HttpClient.GetAsync(uri);

                if (result.IsSuccessStatusCode)
                {
                    var resultModel = await result.Content.ReadFromJsonAsync<ShopModel>();
                    ItemsList = resultModel.Items;
                    PagesCount = resultModel.PagesCount;
                    isLoading = false;
                }
                else
                {
                    isLoading = false;
                    throw new Exception("Fetch data error");
                }
            }
        }

        protected async Task OnFilterApplied(ModulesShopFilterModel filterModel)
        {
            var queryParams = new Dictionary<string, string>();

            queryParams.Add("page", "1");

            foreach(var param in QueryMapHelper.GetDictionaryFromModel(filterModel))
            {
                queryParams.Add(param.Key, param.Value);
            }

            var uri = new Uri(QueryHelpers.AddQueryString(ApiUrl, queryParams));

            isLoading = true;

            var result = await HttpClient.GetAsync(uri);

            if (result.IsSuccessStatusCode)
            {
                var resultModel = await result.Content.ReadFromJsonAsync<ShopModel>();
                ItemsList = resultModel.Items;
                PagesCount = resultModel.PagesCount;
                CurrentPage = 1;
                await JsRuntime.InvokeVoidAsync("ChangeQueryString", uri.Query);
                isLoading = false;
            }
            else
            {
                isLoading = false;
                throw new Exception("Fetch data error");
            }
        }
        protected async Task OnSearch()
        {
            var queryParams = new Dictionary<string, string>();

            queryParams.Add("page", "1");

            queryParams.Add("search", Search);

            var currentLocation = await JsRuntime.InvokeAsync<string>("GetWindowLocation");

            Dictionary<string, string> currentParameters = QueryMapHelper.NameValuesToDictionary(HttpUtility.ParseQueryString(new Uri(currentLocation).Query));

            var uri = new Uri(QueryHelpers.AddQueryString(ApiUrl, QueryMapHelper.UpdateDictionary(currentParameters, queryParams)));

            isLoading = true;

            var result = await HttpClient.GetAsync(uri);


            if (result.IsSuccessStatusCode)
            {
                var resultModel = await result.Content.ReadFromJsonAsync<ShopModel>();
                ItemsList = resultModel.Items;
                PagesCount = resultModel.PagesCount;
                CurrentPage = 1;
                await JsRuntime.InvokeVoidAsync("ChangeQueryString", uri.Query);
                isLoading = false;
            }
            else
            {
                isLoading = false;
                throw new Exception("Fetch data error");
            }
        }
        protected async Task OnPageChange(int newPage)
        {
            var queryParams = new Dictionary<string, string>();

            queryParams.Add("page", newPage.ToString());

            var currentLocation = await JsRuntime.InvokeAsync<string>("GetWindowLocation");

            Dictionary<string, string> currentParameters = QueryMapHelper.NameValuesToDictionary(HttpUtility.ParseQueryString(new Uri(currentLocation).Query));

            var uri = new Uri(QueryHelpers.AddQueryString(ApiUrl, QueryMapHelper.UpdateDictionary(currentParameters, queryParams)));

            isLoading = true;

            var result = await HttpClient.GetAsync(uri);


            if (result.IsSuccessStatusCode)
            {
                var resultModel = await result.Content.ReadFromJsonAsync<ShopModel>();
                ItemsList = resultModel.Items;
                PagesCount = resultModel.PagesCount;
                CurrentPage = newPage;
                await JsRuntime.InvokeVoidAsync("ChangeQueryString", uri.Query);
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
