using System.ComponentModel.DataAnnotations;
namespace ManagerManagement.BL.Domain;
public class DeveloperTask
{
    [Key]
    public int  Id { get; set; }
    
    [Required(ErrorMessage = "Task name is required!")]
    public string TaskName { get; set; }
    
    // Foreign key properties for Assignment and Developer
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    
    public int DeveloperId { get; set; }
    public Developer Developer { get; set; }

    public DeveloperTask()
    {
    }

    public DeveloperTask(int id,String taskName, int projectId, Project project, int developerId, Developer developer)
    {
        Id = id;
        TaskName = taskName;
        ProjectId = projectId;
        Project = project;
        DeveloperId = developerId;
        Developer = developer;
    }
    
    public override string ToString()
    {
        return $"Task ID: {Id}\nTask Name: {TaskName}\nProject ID: {ProjectId}" +
               $"\nDeveloper ID: {DeveloperId}";
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        if (string.Equals(TaskName, "Invalid Name", StringComparison.OrdinalIgnoreCase))
        {
            results.Add(new ValidationResult("Invalid Task name!", new []{nameof(TaskName)}));
        }
        return results;
    }
    
}