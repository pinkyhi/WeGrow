using IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Security.Claims;
using WeGrow.Client.Services;
using WeGrow.Core.Resources;
using WeGrow.Models.Entities;
using WeGrow.Models.SystemInstances;

namespace WeGrow.Client.Pages.SystemInstance
{
    partial class SystemInstances
    {
        public List<SystemInstanceEntity> SystemsList = new();
        public List<ModuleInstanceViewModel> ModulesList = new();

        public string ApiUrl { get; set; }
        private bool modulesLoading { get; set; } = true;
        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private IHttpContextAccessor Accessor { get; set; }
        [Inject] private ITokenService TokenService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            ApiUrl = Configuration["apiUrl"];

            if (ModulesList.Count == 0)
            {
                var tokenResponse = await TokenService.GetToken("WeGrow.read");
                HttpClient.SetBearerToken(tokenResponse.AccessToken);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, ApiUrl + ApiRoutes.AccountModules);
                HttpResponseMessage result = null;
                try
                {
                    requestMessage.Headers.Add(ConstNames.Uid, Accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    result = await HttpClient.SendAsync(requestMessage);
                }
                catch (NullReferenceException)
                {
                    throw new Exception("Not authorized");
                }
                finally
                {
                    requestMessage.Dispose();
                }

                if (result.IsSuccessStatusCode)
                {
                    var resultModel = await result.Content.ReadFromJsonAsync<List<ModuleInstanceViewModel>>();
                    ModulesList.AddRange(resultModel);
                    modulesLoading = false;
                }
                else
                {
                    modulesLoading = false;
                    throw new Exception("Fetch data error");
                }
            }
        }

        private IEnumerable<ModuleInstanceViewModel> GetModulesBySystemId(string systemId = null)
        {
            if(string.IsNullOrWhiteSpace(systemId))
            {
                return ModulesList.Where(x => string.IsNullOrWhiteSpace(x.System_Id));
            }
            else
            {
                return ModulesList.Where(x => systemId.Equals(x.System_Id));
            }
        }
    }
}
