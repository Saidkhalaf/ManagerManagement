using System.ComponentModel.DataAnnotations;
using ManagerManagement.BL;
using ManagerManagement.BL.Domain;
using ManagerManagement.DAL;
using Moq;

namespace Tests;

public class ManagerTests
{
    private readonly IManager _manager;
    private readonly Mock<IRepository> _repositoryMock;

    public ManagerTests()
    {
        _repositoryMock = new Mock<IRepository>();
        _manager = new Manager(_repositoryMock.Object);
    }

    [Fact]
    public void AddProject_Should_Reject_Invalid_Project_Data()
    {
        //Arrange 
        _repositoryMock
            .Setup(repo => repo.ReadManager(1111))
            .Returns((DeveloperManager)null);
        _repositoryMock
            .Setup(repo => repo.ReadTasksOfProject(It.IsAny<int>()))
            .Returns(Enumerable.Empty<DeveloperTask>());
        
        //Act
        var action = () => _manager.AddProject(1111, new List<DeveloperTask>(), "New Project");
        
        //Assert
        Assert.Throws<ValidationException>(action);
        _repositoryMock
            .Verify(repo => repo.CreateProject(It.IsAny<Project>())
                , Times.Never);
    }
    
    
    [Fact]
    public void AddProject_Should_Accept_Valid_Project_Data_And_Persist_Object()
    {
        //Arrange 
        _repositoryMock
            .Setup(repo => repo.ReadManager(1111))
            .Returns((new DeveloperManager
            {
                Name = "Sabir"
            }));
        _repositoryMock
            .Setup(repo => repo.ReadTasksOfProject(1))
            .Returns(new List<DeveloperTask>
            {
                new DeveloperTask
                {
                    TaskName = "Developing"
                }
            });
        
        //Act
        var createdProject = _manager.AddProject(1111, 
            new List<DeveloperTask> { new DeveloperTask() }, "New Project");
        
        //Assert
        _repositoryMock
            .Verify(repo => repo.CreateProject(It.IsAny<Project>())
                , Times.Once);

        Assert.NotNull(createdProject);
        Assert.Equal(1111, createdProject.ManagerId);
        Assert.Equal("New Project", createdProject.ProjectDescription);
    }
    
}