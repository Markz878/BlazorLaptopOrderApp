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
    public class ManufacturersCOntroller : ControllerBase
    {
        private readonly LaptopsContext laptopsContext;

        public ManufacturersCOntroller(LaptopsContext laptopsContext)
        {
            this.laptopsContext = laptopsContext;
        }

        [HttpGet]
        public IEnumerable<ManufacturerModel> Get()
        {
            var result = laptopsContext.Manufacturers.ToArray();
            return result;
        }
    }
}
