using System.Drawing.Text;
using System.Security.Claims;
using ManagerManagement.BL;
using ManagerManagement.BL.Domain;
using ManagerManagement.UI.MVC.Controllers;
using ManagerManagement.UI.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests;

public class DeveloperManagerControllerTests
{
    private readonly DeveloperManagerController _developerManagerController;
    private readonly Mock<IManager> _managerMock;
    private readonly Mock<UserManager<IdentityUser>> _userManagerMock;

    public DeveloperManagerControllerTests()
    {
        _managerMock = new Mock<IManager>();
        _userManagerMock = GetMockUserManager<IdentityUser>();
        _developerManagerController = new DeveloperManagerController(_managerMock.Object, _userManagerMock.Object);
    }

    [Fact]
    public void Add_Should_Return_Same_Page_ViewResult_When_Invalid_ModelState()
    {
        //Arrange 
        _managerMock.Setup(mgr => mgr.GetAllManagers())
            .Returns(new List<DeveloperManager>
            {
                new DeveloperManager
                {
                    Id = 11111111,
                    Name = "Antoon"
                }
            });
        _developerManagerController.ModelState.AddModelError("Department", "Department is required");
        
        //Act
        var result = _developerManagerController.Add(new NewManagerModel());
        
        //Assert
        Assert.IsType<ViewResult>(result);
        var viewResult = (ViewResult)result;
        Assert.Null(viewResult.ViewName);
        _managerMock.Verify(
            mgr => mgr.AddManager(It.IsAny<string>(), It.IsAny<int>(),
                It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<ProjectStatus>(), It.IsAny<DateTime>(), It.IsAny<double>()),
            Times.Never);
    }
    
    [Fact]
    public void Add_Should_Return_Same_Page_ViewResult_And_Persist_DeveloperManager_When_Valid_ModelState()
    {
        //Arrange 
        _managerMock.Setup(mgr => mgr.GetAllManagers())
            .Returns(new List<DeveloperManager>
            {
                new DeveloperManager
                {
                    Id = 11111111,
                    Name = "Antoon"
                }
            });
        _userManagerMock.Setup(uMgr => uMgr.GetUserId(It.IsAny<ClaimsPrincipal>()))
            .Returns("hello-id");
        
        //Act
        var result = _developerManagerController.Add(new NewManagerModel
        {
            Id = 12345, 
            Name = "John", 
            Department = "Engineering", 
            Email = "john.doe@example.com", 
            ProjectStatus = ProjectStatus.InProgress, 
            HireDate = new DateTime(2022, 02, 19), 
            Salary = 50000 
        });
        
        //Assert
        Assert.IsType<ViewResult>(result);
        var viewResult = (ViewResult)result;
        Assert.NotNull(viewResult.ViewName); // Het is not null, Het is : AddManagerConfirmation
        _managerMock.Verify(
            mgr => mgr.AddManager("hello-id", 12345, "John", "Engineering",  ProjectStatus.InProgress,
                new DateTime(2022, 02, 19), 50000 ),
            Times.Once);
    }
    
    private Mock<UserManager<TUser>> GetMockUserManager<TUser>()
        where TUser : class
    {
        var userManagerMock = new Mock<UserManager<TUser>>(
            new Mock<IUserStore<TUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<TUser>>().Object,
            new IUserValidator<TUser>[0],
            new IPasswordValidator<TUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<TUser>>>().Object);

        return userManagerMock;
    }
}