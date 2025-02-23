using ManagerManagement.BL.Domain;

namespace ManagerManagement.UI.MVC.Models.Dto;

public class DeveloperDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string ProgrammingLanguage { get; set; }
    public ICollection<DeveloperTask> AssignedTasks { get; set; }
}