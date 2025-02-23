using ManagerManagement.BL;
using ManagerManagement.BL.Domain;
using ManagerManagement.UI.MVC.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerManagement.UI.MVC.Controllers.API;

[Route("/api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IManager _manager;

    public ProjectsController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet]
    public IEnumerable<Project> GetAllProjects()
    {
        var projects = _manager.GetAllProjects();
        return projects;
    }

    [HttpGet("{managerId}")]
    public ActionResult<IEnumerable<ProjectDto>> GetProjectsOfManager(int managerId)
    {
        var projects = _manager.GetAllProjectsByManager(managerId);
        if (projects == null)
        {
            return NotFound();
        }

        return Ok(projects.Select(project => new ProjectDto
        {
            Id = project.Id,
            ManagerId = project.ManagerId,
            ProjectTasks = project.ProjectTasks,
            ProjectDescription = project.ProjectDescription
        }));
    }
    
    [HttpPost]
    public ActionResult<ProjectDto> AddNewProject(NewProjectDto projectDto)
    {
        
        var createdProject = _manager.AddProject(projectDto.ManagerId, projectDto.Tasks, projectDto.ProjectDescription);

        return CreatedAtAction("GetProjectsOfManager",new {Id = createdProject.Id},new ProjectDto
        {
            Id = createdProject.Id,
            ManagerId = createdProject.ManagerId,
            ProjectTasks = createdProject.ProjectTasks,
            ProjectDescription = createdProject.ProjectDescription
        });
    }
    
}