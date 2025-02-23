using ManagerManagement.BL;
using ManagerManagement.UI.MVC.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ManagerManagement.UI.MVC.Controllers.API;

[Route("/api/[controller]")]
[ApiController]
public class DeveloperManagersController : ControllerBase
{
    private readonly IManager _manager;
    private readonly UserManager<IdentityUser> _userManager;

    public DeveloperManagersController(IManager manager, UserManager<IdentityUser> userManager)
    {
        _manager = manager;
        _userManager = userManager;
    }


    [HttpGet]
    public IActionResult GetAllDeveloperManagers()
    {
        var managers = _manager.GetAllManagers().ToList();
        if (!managers.Any())
        {
            return NoContent();
        }
        
        return Ok(managers.Select(manager => new DeveloperManagerDto
        {
            Id = manager.Id,
            Name = manager.Name,
            Department = manager.Department,
            Salary = manager.Salary
        }));
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<DeveloperManagerDto>> UpdateManager(int id,
        [FromBody] UpdateManagerDto updateManagerDto)
    {
        var loggedInUser = await _userManager.GetUserAsync(User);

        var manager = _manager.GetManager(id);
        if (manager == null)
        {
            return NotFound(); // Return a 404 Not Found if the manager with the specified id is not found
        }
        
        if (User.IsInRole(CustomIdentityConstants.AdminRole) || manager.ManagerUser == loggedInUser)
        {
            manager.Salary = updateManagerDto.Salary;
            _manager.UpdateManager(manager.Id, updateManagerDto.Salary);
            
        }

        else if (manager.ManagerUser != loggedInUser)
        {
            return Forbid(); // Return a 403 Forbidden if the logged-in user is not responsible for the Manager
        }
        return NoContent();
    }
}
