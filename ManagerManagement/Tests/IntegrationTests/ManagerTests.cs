using System.ComponentModel.DataAnnotations;
using System.Data;
using ManagerManagement.BL;
using ManagerManagement.BL.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Tests.IntegrationTests;

public class ManagerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ManagerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void GetManagerWithDevelopers_Should_Fetch_Related_Data()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IManager>();
    
        //Act
        var developerManager = manager.GetManagerWithDevelopers(11111);
        
       //Assert
       Assert.Equal(11111, developerManager.Id);
       Assert.Equal("HR", developerManager.Department);
       
       //Assert related Projects
       Assert.Equal(1, developerManager.ManagedProjects.Count);
       var project = developerManager.ManagedProjects.Single();
       Assert.Equal(11111, project.ManagerId);
       Assert.NotEmpty(developerManager.ManagedProjects);
       Assert.Contains(developerManager.ManagedProjects, p => p.ProjectDescription == "Project 4 Description");
    
    }

    [Fact]
    public void AddProject_Should_Validate_The_ManagerId()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IManager>();
        
        //Act
        var tasks = new List<DeveloperTask>
        {
            new DeveloperTask { Id = 6, ProjectId = 5, DeveloperId = 222222, TaskName = "Task 1" },
            new DeveloperTask { Id = 7, ProjectId = 5, DeveloperId = 222222, TaskName = "Task 2" }
        };
        var action = () => manager
            .AddProject(0, tasks, "New Project"); //ManagerId is 0
        
        //Assert
        Assert.Throws<ValidationException>(action);
    }
}