using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Tests.IntegrationTests.config;

namespace Tests.IntegrationTests;

public class DeveloperManagerControllerTests 
    : IClassFixture<CustomWebApplicationFactoryWithMockAuth<Program>>
{

    private readonly CustomWebApplicationFactoryWithMockAuth<Program> _factory;

    public DeveloperManagerControllerTests(CustomWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Add_Manager_Should_Require_Authenticated_User()
    {
        //Arrange 
        var httpClient = _factory.CreateClient(
            new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        
        //Act
        var response = httpClient.PostAsync("/DeveloperManager/Add", null).Result;
        
        //Assert
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Equal("/Identity/Account/Login", response.Headers.Location?.AbsolutePath);
    }

    [Fact]
    public void Add_Manager_Should_Not_Accept_Invalid_Input()
    {
        //Arrange
        var httpClient = _factory
            .AuthenticatedInstance(
                new Claim(ClaimTypes.NameIdentifier, "said@kdg.be"))
            .CreateClient();
        
        //Act
        var response = httpClient.PostAsync("/DeveloperManager/Add", null)
            .Result;
        var content = response.Content.ReadAsStringAsync().Result;
        
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
    }
    
    [Fact]
    public void Add_Manager_Should_Accept_Valid_Input()
    {
        //Arrange
        var httpClient = _factory
            .AuthenticatedInstance(
                new Claim(ClaimTypes.NameIdentifier, "said@kdg.be"))
            .CreateClient();
        
        //Act
        var response = httpClient.PostAsync("/DeveloperManager/Add", 
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "Id", "111111111"},
                    { "Name", "Hashem"},
                    { "Department", "HR"},
                    { "ProjectStatus", "ProjectStatus.Completed"},
                    { "Hire Date", "11/14/2022"},
                    { "Salary", "898999"},
                    { "Email", "hashem@HR.be"}
                }))
            .Result;
        var content = response.Content.ReadAsStringAsync().Result;
        
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
    }
    
}