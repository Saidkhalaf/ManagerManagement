using System.Collections;
using ManagerManagement.BL;
using ManagerManagement.BL.Domain;
using ManagerManagement.UI.MVC.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ManagerManagement.UI.MVC.Controllers.API;

[Route("/api/[controller]")]
[ApiController]
public class DeveloperTasksController : ControllerBase
{
    private readonly IManager _manager;

    public DeveloperTasksController(IManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IEnumerable<DeveloperTask> GetAllDeveloperTasks()
    {
        var tasks = _manager.GetAllDeveloperTasks();
        return tasks;
    }

    [HttpGet("{projectId}")]
    public ActionResult<IEnumerable<DeveloperTaskDto>> GetTasksOfProject(int projectId)
    {
        var tasks = _manager.GetTasksOfProject(projectId).ToList();
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

    [HttpPost]
    public ActionResult<DeveloperTaskDto> AddTaskTOProject(NewDeveloperTaskDto newTaskDto)
    {

        var createdTask = _manager.AddTaskToProject(newTaskDto.ProjectId ,newTaskDto.DeveloperId,newTaskDto.TaskName);

        return CreatedAtAction("GetTasksOfProject", new { id = createdTask.Id },
            new DeveloperTaskDto
            {
                Id = createdTask.Id,
                TaskName = createdTask.TaskName,
                DeveloperId = createdTask.DeveloperId,
                ProjectId = createdTask.ProjectId
            });
    }
}