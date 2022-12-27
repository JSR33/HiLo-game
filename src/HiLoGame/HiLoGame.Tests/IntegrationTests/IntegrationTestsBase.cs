using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using HiLoGame.Backend;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HiLoGame.Backend.Data;
using Microsoft.Extensions.DependencyInjection;

namespace HiLoGame.Tests.IntegrationTests
{
    public class IntegrationTestsBase
    {
        protected readonly HttpClient TestClient;

        public IntegrationTestsBase()
        {
            TestClient = new WebApplicationFactory<Program>().CreateClient();
        }
    }
}
