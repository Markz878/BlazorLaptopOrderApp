using BlazorWebAssembyDemoApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWebAssembyDemoApp.Client.Pages
{
    [Authorize]
    public partial class FetchData
    {
        [Inject] public IAccessTokenProvider AuthenticationService { get; set; } 
        [Inject] public NavigationManager Navigation { get; set; }

        private WeatherForecast[] forecasts;
        protected override async Task OnInitializedAsync()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(Navigation.BaseUri)
            };

            var tokenResult = await AuthenticationService.RequestAccessToken();
            if (tokenResult.TryGetToken(out var token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");
                forecasts = await httpClient.GetJsonAsync<WeatherForecast[]>("api/weatherforecast");
            }
            else
            {
                Navigation.NavigateTo(tokenResult.RedirectUrl);
            }

        }
    }
}
