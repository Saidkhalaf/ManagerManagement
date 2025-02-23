using System.Collections;
using System.Collections.Immutable;
using ManagerManagement.BL;
using ManagerManagement.BL.Domain;
using ManagerManagement.UI.MVC.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ManagerManagement.UI.MVC.Controllers.API;

[Route("/api/[controller]")]
[ApiController]
public class DevelopersController : ControllerBase
{
    private readonly IManager _manager;

    public DevelopersController(IManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IEnumerable<Developer> GetAllDevelopers()
    {
        var developers = _manager.GetAllDevelopers();
        return developers;
    }

    [HttpGet("{developerId}")]
    public ActionResult<IEnumerable<DeveloperTaskDto>> GetTasksOfDeveloper(long developerId)
    {
        var tasks = _manager.GetTasksOfDeveloperWithProject(developerId).ToList();
        if (!tasks.Any())
        {
            return NoContent();
        }

        return Ok(tasks.Select(task => new DeveloperTaskDto
        {
            Id = task.Id,
            TaskName = task.TaskName,
            DeveloperId = task.DeveloperId,
            ProjectId = task.ProjectId
        }));
    }
    
}