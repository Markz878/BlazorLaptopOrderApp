using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebAssembyDemoApp.Client.Pages
{
    [Authorize]
    public partial class Counter
    {
        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount++;
        }
    }
}
