namespace BlazorWebAssembyDemoApp.Shared
{
    public class LaptopModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ManufacturerModel Manufacturer { get; set; }
        public string Processor { get; set; }
        public string GPU { get; set; }
        public int Ram { get; set; }
        public int Memory { get; set; }
        public string ImageUrl { get; set; }
        public int Rating { get; set; }
    }
}
