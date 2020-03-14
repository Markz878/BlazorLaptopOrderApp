using BlazorWebAssembyDemoApp.Client.Models;
using BlazorWebAssembyDemoApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWebAssembyDemoApp.Client.Pages
{
    [Authorize]
    public partial class Laptops
    {

        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public IAccessTokenProvider AuthenticationService { get; set; }
        public LaptopCompareModel[] LaptopList { get; set; }
        public ManufacturerViewModel[] Manufacturers { get; set; }
        public LaptopCompareModel SelectedLaptop { get; set; }
        public ManufacturerViewModel SelectedManufacturer { get; set; }
        public UserModel User { get; set; } = new UserModel();
        public bool[] CompareList { get; set; }
        PageState State { get; set; } = PageState.Listing;
        enum PageState
        {
            Listing,
            Comparing,
            Ordered,
        }

        protected async override Task OnInitializedAsync()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(Navigation.BaseUri)
            };

            var tokenResult = await AuthenticationService.RequestAccessToken();
            if (tokenResult.TryGetToken(out var token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");
                var laptopList = await httpClient.GetJsonAsync<LaptopModel[]>("api/laptops");
                LaptopList = laptopList.Select(x => new LaptopCompareModel() { Id = x.Id, Name = x.Name, Manufacturer = x.Manufacturer, GPU = x.GPU, ImageUrl = x.ImageUrl, Memory = x.Memory, Price = x.Price, Processor = x.Processor, Ram = x.Ram, Rating=x.Rating }).ToArray();
                var manufacturers = await httpClient.GetJsonAsync<ManufacturerModel[]>("api/manufacturers");
                Manufacturers = manufacturers.Select(x => new ManufacturerViewModel() { Id = x.Id, Name = x.Name, ImageUrl = x.ImageUrl, CSSClass= "manufacturer-image" }).ToArray();
            }
            else
            {
                Navigation.NavigateTo(tokenResult.RedirectUrl);
            }
        }

        void UpdateCompareValues()
        {
            StateHasChanged();
        }

        void ReturnToList()
        {
            SelectedLaptop = null;
            State = PageState.Listing;
        }

        void CompareSelectedLaptops()
        {
            State = PageState.Comparing;
        }

        void SelectManufacturer(int manufacturerId)
        {
            if (SelectedManufacturer==null)
            {
                SelectedManufacturer = Manufacturers.First(x => x.Id == manufacturerId);
                SelectedManufacturer.CSSClass = "manufacturer-image-selected";
            }
            else if (manufacturerId == SelectedManufacturer.Id)
            {
                SelectedManufacturer.CSSClass = "manufacturer-image";
                SelectedManufacturer = null;
            }
            else
            {
                SelectedManufacturer.CSSClass = "manufacturer-image";
                SelectedManufacturer = Manufacturers.First(x => x.Id == manufacturerId);
                SelectedManufacturer.CSSClass = "manufacturer-image-selected";
            }
        }

        void SubmitOrder()
        {
            State = PageState.Ordered;
        }

        void ClearSelectedLaptops()
        {
            foreach (var laptop in LaptopList)
            {
                if (laptop.Compare)
                {
                    laptop.Compare = false;
                }
            }
        }
        void SelectLaptop(LaptopCompareModel laptop)
        {
            SelectedLaptop = laptop;
        }
    }
}
