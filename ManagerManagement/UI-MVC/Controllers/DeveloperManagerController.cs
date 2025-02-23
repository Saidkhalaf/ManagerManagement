using ManagerManagement.BL;
using ManagerManagement.UI.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ManagerManagement.UI.MVC.Controllers;
public class DeveloperManagerController : Controller
{
    private readonly IManager _manager;
    private readonly UserManager<IdentityUser> _userManager;

    public DeveloperManagerController(IManager manager, UserManager<IdentityUser> userManager)
    {
        _manager = manager;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        var managers = _manager.GetAllManagers();
        return View(managers);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [Authorize]  
    public IActionResult Add(NewManagerModel manager)
    {
        if (!ModelState.IsValid)
        {
            return View(manager);
        }

        var userId = _userManager.GetUserId(User);

        _manager.AddManager(userId,manager.Id, manager.Name, manager.Department, manager.ProjectStatus, manager.HireDate, manager.Salary);
        ModelState.Clear();
            
        return View("AddManagerConfirmation", new AddManagerConfirmationModel
        {
            Id = manager.Id,
            Name = manager.Name,
            Department = manager.Department
        });
    }

    public IActionResult Details(long id)
    {
        var manager = _manager.GetManagerWithDevelopers(id);
        return View(manager);
    }
    
    public IActionResult AddManagerConfirmation(NewManagerModel manager)
    {
        var vm = new AddManagerConfirmationModel();
        vm.Id = manager.Id;
        vm.Name = manager.Name;
        vm.Department = manager.Department;

        return View(vm);
    }
}