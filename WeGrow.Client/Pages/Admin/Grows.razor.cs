using Microsoft.AspNetCore.Components;
using WeGrow.Core.Resources;
using WeGrow.Models.Entities;

namespace WeGrow.Client.Pages.Admin
{
    partial class Grows
    {
        public List<GrowEntity> ItemsList = new();
        public string ApiUrl { get; set; }

        [Inject] private IConfiguration Configuration { get; set; }

        protected override void OnInitialized()
        {
            ApiUrl = Configuration["apiUrl"] + ApiRoutes.AdminGrows;
        }
    }
}
