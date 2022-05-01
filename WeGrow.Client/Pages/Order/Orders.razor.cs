using IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
using System.Web;
using WeGrow.Client.Services;
using WeGrow.Core.Helpers;
using WeGrow.Core.Resources;
using WeGrow.Models.Entities;
using WeGrow.Models.Order;
using WeGrow.Models.Shop;

namespace WeGrow.Client.Pages.Order
{
    partial class Orders
    {
        public List<OrderModel> ItemsList = new();

        public string ApiUrl { get; set; }

        private bool isLoading = true;
        private bool redirectToLogin = false;
        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }
        [Inject] private IHttpContextAccessor Accessor { get; set; }
        [Inject] private ITokenService TokenService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }



        protected override async Task OnInitializedAsync()
        {
            ApiUrl = Configuration["apiUrl"] + ApiRoutes.Orders;

            if (ItemsList.Count == 0)
            {
                var tokenResponse = await TokenService.GetToken("WeGrow.read");
                HttpClient.SetBearerToken(tokenResponse.AccessToken);

                isLoading = true;

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, ApiUrl);
                HttpResponseMessage result = null;
                try
                {
                    requestMessage.Headers.Add(ConstNames.Uid, Accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    result = await HttpClient.SendAsync(requestMessage);
                }
                catch (NullReferenceException)
                {
                    redirectToLogin = true;
                    return;
                }
                finally
                {
                    requestMessage.Dispose();
                }

                if (result.IsSuccessStatusCode)
                {
                    var resultModel = await result.Content.ReadFromJsonAsync<List<OrderModel>>();
                    ItemsList.AddRange(resultModel);
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
}
