using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using ManagerManagement.UI.MVC.Models.Dto;
using Tests.IntegrationTests.config;

namespace Tests.IntegrationTests;

public class ClientsControllerTests 
    : IClassFixture<CustomWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly CustomWebApplicationFactoryWithMockAuth<Program> _factory;

    public ClientsControllerTests(CustomWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void AddClient_Should_Return_Unauthorized_If_Not_Signed_In()
    {
        //Arrange 
        var httpClient = _factory.CreateClient();
        
        //Act
        var response = httpClient.PostAsync("/api/clients", null).Result;
        
        //Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public void AddClient_Should_Return_BadRequest_When_Missing_Name()
    {
        //Arrange
        var httpClient = _factory
            .AuthenticatedInstance(
                new Claim(ClaimTypes.NameIdentifier, "said"))
            .CreateClient();
        var httpBody = new StringContent(
            "{ \"name\" : null }", Encoding.UTF8, "application/json");
        
        //Act
        var response = httpClient.PostAsync(
            "api/clients", httpBody).Result;
        
        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public void AddClient_Should_Return_CreatedClient_When_Request_Valid()
    {
        //Arrange
        var httpClient = _factory
            .AuthenticatedInstance(
                new Claim(ClaimTypes.NameIdentifier, "said"))
            .CreateClient();
        var httpBody = new StringContent(
            JsonSerializer.Serialize(new NewClientDto
            {
                Name = "Said",
                BirthDate = new DateTime(1998, 02 ,17),
                Email = "said.khalaf1998@gmail.com",
                ManagerId = 1111
            }),
            Encoding.UTF8, "application/json");
        
        //Act
        var response = httpClient.PostAsync(
            "api/clients", httpBody).Result;
        
        //Assert
        
        var responseBody = response.Content.ReadAsStringAsync().Result;
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Matches("{\"id\":\\d,\"name\":\"Said\",\"birthDate\":\"1998-02-17T00:00:00\",\"email\":\"said.khalaf1998@gmail.com\"}", responseBody);
        Assert.Equal("/api/Clients/333334", response.Headers.Location?.PathAndQuery);
    }
    
}