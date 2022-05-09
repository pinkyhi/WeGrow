using IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
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
        public string ApiUrl { get; set; }
        private bool modulesLoading { get; set; } = true;
        private bool systemsLoading { get; set; } = true;

        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private IHttpContextAccessor Accessor { get; set; }
        [Inject] private ITokenService TokenService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ApiUrl = Configuration["apiUrl"];

            if (ModulesList.Count == 0 && SystemsList.Count == 0)
            {
                var tokenResponse = await TokenService.GetToken("WeGrow.read");
                HttpClient.SetBearerToken(tokenResponse.AccessToken);

                var modelsRequestMessage = new HttpRequestMessage(HttpMethod.Get, ApiUrl + ApiRoutes.AccountModules);
                var systemsRequestMessage = new HttpRequestMessage(HttpMethod.Get, ApiUrl + ApiRoutes.AccountSystems);

                HttpResponseMessage modulesResult = null;
                HttpResponseMessage systemsResult = null;

                try
                {
                    modelsRequestMessage.Headers.Add(ConstNames.Uid, Accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    systemsRequestMessage.Headers.Add(ConstNames.Uid, Accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    modulesResult = await HttpClient.SendAsync(modelsRequestMessage);
                    systemsResult = await HttpClient.SendAsync(systemsRequestMessage);
                }
                catch (NullReferenceException)
                {
                    throw new Exception("Not authorized");
                }
                finally
                {
                    modelsRequestMessage.Dispose();
                    systemsRequestMessage.Dispose();
                }
                if (modulesResult.IsSuccessStatusCode && systemsResult.IsSuccessStatusCode)
                {
                    var modulesResultModel = await modulesResult.Content.ReadFromJsonAsync<List<ModuleInstanceViewModel>>();
                    ModulesList.AddRange(modulesResultModel);
                    modulesLoading = false;
                    var systemsResultModel = await systemsResult.Content.ReadFromJsonAsync<List<SystemInstanceViewModel>>();
                    foreach(var system in systemsResultModel)
                    {
                        system.ModuleInstances.AddRange(ModulesList.Where(i => i.System_Id?.Equals(system.Id) == true));
                    }
                    SystemsList.AddRange(systemsResultModel);
                    systemsLoading = false;
                }
                else
                {
                    modulesLoading = false;
                    systemsLoading = false;
                    throw new Exception("Fetch data error");
                }
            }
        }

        public async Task RemoveSystem(SystemInstanceViewModel system)
        {
            var tokenResponse = await TokenService.GetToken("WeGrow.write");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);
            var uri = QueryHelpers.AddQueryString(ApiUrl + ApiRoutes.AccountSystems, "id", system.Id);
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

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
                foreach(var module in system.ModuleInstances)
                {
                    module.System_Id = null;
                }
                SystemsList.Remove(system);
            }
            else
            {
                throw new Exception("Delete data error");
            }
        }

        public async Task AddSystemWithSchedule()
        {
            var tokenResponse = await TokenService.GetToken("WeGrow.write");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);
            var systemRequestMessage = new HttpRequestMessage(HttpMethod.Post, ApiUrl + ApiRoutes.AccountSystems);
            HttpResponseMessage systemResult = null;
            try
            {
                systemRequestMessage.Headers.Add(ConstNames.Uid, Accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var contentModel = new CreateSystemRequest()
                {
                    Name = CreationModel.Name,
                    ModuleSchedules = CreationModel.ModuleSchedules
                };
                systemRequestMessage.Content = JsonContent.Create(contentModel);
                systemResult = await HttpClient.SendAsync(systemRequestMessage);

            }
            catch (NullReferenceException)
            {
                throw new Exception("Not authorized");
            }
            finally
            {
                systemRequestMessage.Dispose();
            }
            if (systemResult.IsSuccessStatusCode)
            {
                var createdSystem = await systemResult.Content.ReadFromJsonAsync<SystemInstanceViewModel>();
                SystemsList.Add(createdSystem);
                CreationModel.Step = SystemCreationModel.CreationStep.None;
            }
            else
            {
                throw new Exception("Fetch data error");
            }
        }

        public void StartAdding()
        {
            CreationModel.Step = SystemCreationModel.CreationStep.SystemInfo;
        }

        public void CancelAdding()
        {
            CreationModel.Step = SystemCreationModel.CreationStep.None;
            CreationModel.Modules = new();
            CreationModel.Name = null;
        }

        private List<ModuleInstanceViewModel> GetModulesBySystemId(string systemId = null)
        {
            if(string.IsNullOrWhiteSpace(systemId))
            {
                return ModulesList.Where(x => string.IsNullOrWhiteSpace(x.System_Id)).ToList();
            }
            else
            {
                return ModulesList.Where(x => systemId.Equals(x.System_Id)).ToList();
            }
        }
    }
}
