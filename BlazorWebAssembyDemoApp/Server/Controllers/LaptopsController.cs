using BlazorWebAssembyDemoApp.Server.Data;
using BlazorWebAssembyDemoApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BlazorWebAssembyDemoApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LaptopsController : ControllerBase
    {
        private readonly LaptopsContext laptopsContext;

        public LaptopsController(LaptopsContext laptopsContext)
        {
            this.laptopsContext = laptopsContext;
            LaptopsInitializer.SeedDatabase(laptopsContext);
        }

        [HttpGet]
        public IEnumerable<LaptopModel> Get()
        {
            var result = laptopsContext.Laptops.Include(x => x.Manufacturer).ToArray();
            return result;
        }
    }
}
