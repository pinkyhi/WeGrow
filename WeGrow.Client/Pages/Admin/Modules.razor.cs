﻿using IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using WeGrow.Client.Services;
using WeGrow.Core.Resources;
using WeGrow.Models.Entities;

namespace WeGrow.Client.Pages.Admin
{
    public partial class Modules
    {
        public List<ModuleEntity> ItemsList = new();
        public string ApiUrl { get; set; }
        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private ITokenService TokenService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ApiUrl = Configuration["apiUrl"] + ApiRoutes.AdminModules;
            var tokenResponse = await TokenService.GetAdminToken();
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            var result = await HttpClient.GetAsync(ApiUrl);

            if (result.IsSuccessStatusCode)
            {
                ItemsList = await result.Content.ReadFromJsonAsync<List<ModuleEntity>>();
            }
        }
    }
}
