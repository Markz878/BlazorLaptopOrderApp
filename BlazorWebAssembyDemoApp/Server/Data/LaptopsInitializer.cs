using BlazorWebAssembyDemoApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorWebAssembyDemoApp.Server.Data
{

    public static class LaptopsInitializer
    {
        static readonly Random rng = new Random();

        public static void SeedDatabase(LaptopsContext context)
        {
            context.Database.EnsureCreated();
            if (context.Laptops.Any() && context.Manufacturers.Any())
            {
                return;
            }
            var manufacturers = new ManufacturerModel[]
            {
                new ManufacturerModel(){ Name = "Acer", ImageUrl="https://1000logos.net/wp-content/uploads/2016/09/Acer-logo.png" },
                new ManufacturerModel(){ Name = "Asus", ImageUrl="https://1000logos.net/wp-content/uploads/2016/10/Asus-Logo.png" },
                new ManufacturerModel(){ Name = "Dell", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/8/82/Dell_Logo.png" },
                new ManufacturerModel(){ Name = "HP", ImageUrl="https://www.pngkey.com/png/full/74-748780_hp-logo-png-corporate-welln-hewlett-packard-current.png" },
                new ManufacturerModel(){ Name = "Lenovo", ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/0/03/Lenovo_Global_Corporate_Logo.png/1200px-Lenovo_Global_Corporate_Logo.png" },
                new ManufacturerModel(){ Name = "MSI", ImageUrl="https://logos-download.com/wp-content/uploads/2019/11/Msi_Logo.png" },
            };
            var laptops = new LaptopModel[100];
            for (int i = 0; i < laptops.Length; i++)
            {
                laptops[i] = new LaptopModel()
                {
                    Manufacturer = manufacturers[rng.Next(manufacturers.Length)],
                    Memory = rng.Next(1, 5) * 256,
                    Price = rng.Next(750, 1500),
                    Processor = GetRandomProcessor(),
                    Ram = Convert.ToInt32(Math.Pow(2, Convert.ToDouble(rng.Next(1, 4)))),
                    GPU = GetRandomGPU(),
                    ImageUrl = GetLaptopImage(),
                    Rating = rng.Next(1,6),
                };
                laptops[i].Name = GetLaptopName(laptops[i].Manufacturer.Name);
            }

            foreach (var m in manufacturers)
            {
                context.Manufacturers.Add(m);
            }
            foreach (var l in laptops)
            {
                context.Laptops.Add(l);
            }
            context.SaveChanges();
        }

        static string GetRandomProcessor()
        {
            var processorTypes = new string[]
            {
                "Intel Core i3",
                "Intel Core i5",
                "Intel Core i7",
                "AMD Ryzen 5",
                "AMD Ryzen 7",
            };

            return processorTypes[rng.Next(0, 2)] + "-" + rng.Next(1000, 9000);
        }

        static string GetRandomGPU()
        {
            var gpuTypes = new string[]
            {
                "NVIDIA GeForce GTX 1050 4 Gt GDDR6",
                "NVIDIA GeForce RTX 2060 6 Gt GDDR6",
                "NVIDIA GeForce MX250",
                "NVIDIA GeForce GTX 1650 4 Gt",
                "AMD Radeon RX Vega 10",
            };

            return gpuTypes[rng.Next(0, gpuTypes.Length)];
        }

        static string GetLaptopName(string manufacturer)
        {
            Dictionary<string, string[]> names = new Dictionary<string, string[]>
            {
                { "Acer", new string[] { "Aspire", "Nitro" } },
                { "Asus", new string[] { "Chromebook", "ROG Strix", "Zenbook" } },
                { "Dell", new string[] { "Inspiron", "Latitude", "XPS" } },
                { "HP", new string[] { "Envy", "Omen", "Palivion", "Spectre" } },
                { "Lenovo", new string[] { "Ideapad", "Legion", "Thinkpad", "Yoga" } },
                { "MSI", new string[] { "GF63", "GL65" } },
            };
            var namelist = names[manufacturer];
            return namelist[rng.Next(namelist.Length)];
        }

        static string GetLaptopImage()
        {
            var images = new string[]
            {
                "https://cdn.verk.net/1440/images/99/2_549628-2791x2168.jpeg",
                "https://cdn.verk.net/1440/images/96/2_549772-2892x2346.jpg",
                "https://cdn.verk.net/1440/images/42/2_593950-4000x2149.jpg",
                "https://cdn.verk.net/1440/images/66/2_552274-2558x2152.jpg",
                "https://cdn.verk.net/1440/images/15/2_555802-3000x1554.jpg",
                "https://cdn.verk.net/1440/images/84/2_579004-3908x3052.jpg",
                "https://cdn.verk.net/1440/images/64/2_579079-4000x2072.jpg",
                "https://cdn.verk.net/1440/images/52/2_580549-2999x1509.jpeg",
                "https://cdn.verk.net/1440/images/30/2_580495-4000x2273.jpg",
                "https://www.gigantti.fi/image/dv_web_D180001002370312/105197/dell-g3-15-3590-156-pelikannettava.jpg?$prod_all4one$",
                "https://www.gigantti.fi/image/dv_web_D180001002370312/105197/dell-g3-15-3590-156-pelikannettava.jpg?$prod_all4one$",
                "https://www.gigantti.fi/image/dv_web_D180001002192104/13342/dell-latitude-7390-133-kannettava-musta.jpg?$prod_all4one$",
                "https://www.gigantti.fi/image/dv_web_D180001002257501/32636/hp-pavilion-gaming-15-dk0904no-156-pelikannettava.jpg?$prod_all4one$"
            };

            return images[rng.Next(images.Length)];
        }
    }
}