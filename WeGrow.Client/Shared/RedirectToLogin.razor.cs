using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WeGrow.Client.Shared
{
    public partial class RedirectToLogin
    {
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public IHttpContextAccessor Accessor { get; set; }

        protected async override Task OnInitializedAsync()
        {
            // TODO: Redirect if problem is not in admin rights
            Navigation.NavigateTo($"/login?redirectUri={Uri.EscapeDataString(Navigation.Uri)}", true);
        }
    }
}
