using IdentityModel.Client;
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

        [Inject] private IConfiguration Configuration { get; set; }

        protected override void OnInitialized()
        {
            ApiUrl = Configuration["apiUrl"] + ApiRoutes.AdminModules;
        }
    }
}
