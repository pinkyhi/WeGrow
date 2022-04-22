using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using IdentityModel.Client;
using Microsoft.AspNetCore.WebUtilities;
using WeGrow.Client.Services;
using WeGrow.Core.Enums;
using System.Globalization;
using WeGrow.Models.Entities;
using Microsoft.AspNetCore.Components;

namespace WeGrow.Client.Shared
{
    partial class AdminTableCRUD<TItem>
    {
        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private ITokenService TokenService { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }

        [Parameter]
        public string TableTitle { get; set; }

        [Parameter]
        public string ApiUrl { get; set; }

        [Parameter, AllowNull]
        public List<TItem> Items { get; set; }

        private bool IsLoading;
        private Dictionary<TItem, TItem> ChangedItems { get; set; } = new();

        private IEnumerable<PropertyInfo> Properties { get; set; }
        private IEnumerable<PropertyInfo> IdProperties { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            var tokenResponse = await TokenService.GetAdminToken();
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            var result = await HttpClient.GetAsync(ApiUrl);

            if (result.IsSuccessStatusCode)
            {
                Items = await result.Content.ReadFromJsonAsync<List<TItem>>();
                Items.Insert(0, new TItem());
                IsLoading = false;
            }
            else
            {
                IsLoading = false;
                throw new Exception("Fetch data error");
            }
        }
        private bool CompareItemIds(TItem item1, TItem item2)
        {
            bool result = true;
            foreach (var prop in IdProperties)
            {
                result = prop.GetValue(item1).Equals(prop.GetValue(item2));
                if (!result)
                {
                    return result;
                }
            }
            return result;
        }

        private bool CompareItems(TItem item1, TItem item2)
        {
            bool result = true;
            foreach (var prop in Properties)
            {
                result = (prop.GetValue(item1)?.Equals(prop.GetValue(item2) ?? prop.GetValue(item1) == prop.GetValue(item2)) == true);
                if (!result)
                {
                    return result;
                }
            }
            return result;
        }

        private void CopyItemValues(TItem source, ref TItem destination)
        {
            foreach (var prop in Properties)
            {
                prop.SetValue(destination, prop.GetValue(source));
            }
        }

        private Dictionary<string, string> GetIdValues(TItem item)
        {
            Dictionary<string, string> result = new();
            foreach (var prop in IdProperties)
            {
                result.Add(prop.Name, prop.GetValue(item).ToString());
            }
            return result;
        }
    }
}
