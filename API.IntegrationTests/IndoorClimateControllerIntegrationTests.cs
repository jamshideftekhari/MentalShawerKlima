using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ModelLib.Models;
using Xunit;

namespace API.IntegrationTests
{
    public class IndoorClimateControllerIntegrationTests : ControllerTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyData_ReturnsEmptyResponse()
        {
            //arrange


            //act
            var response = await testClient.GetAsync(URI);
            
            //assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);


        }



    }
}
