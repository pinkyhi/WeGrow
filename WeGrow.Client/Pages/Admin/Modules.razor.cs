using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeGrow.Models.Entities;

namespace WeGrow.Client.Pages.Admin
{
    public partial class Modules
    {
        private List<ModuleEntity> ModulesList = new();
        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await HttpClient.GetAsync(Configuration["apiUrl"] + "/admin/modules");

            if (result.IsSuccessStatusCode)
            {
                ModulesList = await result.Content.ReadFromJsonAsync<List<ModuleEntity>>();
            }
        }
    }
}
