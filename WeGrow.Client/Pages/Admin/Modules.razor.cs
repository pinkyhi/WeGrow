﻿using IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeGrow.Client.Services;
using WeGrow.Models.Entities;

namespace WeGrow.Client.Pages.Admin
{
    public partial class Modules
    {
        private List<ModuleEntity> ModulesList = new();
        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private ITokenService TokenService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var tokenResponse = await TokenService.GetToken("WeGrow.read");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            var result = await HttpClient.GetAsync(Configuration["apiUrl"] + "/admin/modules");

            if (result.IsSuccessStatusCode)
            {
                ModulesList = await result.Content.ReadFromJsonAsync<List<ModuleEntity>>();
            }
        }
    }
}