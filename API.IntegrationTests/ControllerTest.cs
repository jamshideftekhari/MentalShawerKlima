using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using ApiMentalShowerIndoor;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ModelLib.Models;
using Xunit;
using ApiMentalShowerIndoor;

namespace API.IntegrationTests
{
    public class ControllerTest
    {
        protected readonly string URI = "http://localhost:5000/api/IndoorClimate";
        protected readonly HttpClient testClient;
        protected ControllerTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(FanContext));
                        services.AddDbContext<FanContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });
            testClient = appFactory.CreateClient();
        }

















    }
}
