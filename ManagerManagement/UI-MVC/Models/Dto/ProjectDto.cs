using ManagerManagement.BL.Domain;

namespace ManagerManagement.UI.MVC.Models.Dto;

public class ProjectDto
{
    public int Id { get; set; }
    
    public int ManagerId { get; set; }

    public ICollection<DeveloperTask> ProjectTasks { get; set; }
    public string ProjectDescription { get; set; }
    
    
    
}