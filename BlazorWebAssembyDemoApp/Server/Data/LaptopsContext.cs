using BlazorWebAssembyDemoApp.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebAssembyDemoApp.Server.Data
{
    public class LaptopsContext : DbContext
    {
        public DbSet<LaptopModel> Laptops { get; set; }
        public DbSet<ManufacturerModel> Manufacturers { get; set; }

        public LaptopsContext(DbContextOptions<LaptopsContext> options) : base(options) { }

    }
}
