using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerManagement.BL.Domain;

public class Project
{
    [Key]
    public int Id { get; set; }
    
    // Foreign key properties en navigatie-eigenschappen
    [Required]
    public int ManagerId { get; set; }
    public DeveloperManager DeveloperManager { get; set; }
    
    public ICollection<DeveloperTask> ProjectTasks { get; set; }
    
    public DateTime ProjectDate { get; set; }
    [Required(ErrorMessage = "Project Description is required!")]
    public string ProjectDescription { get; set; }
    
    public int ContributionOrder { get; set; }

    public Project()
    {
    }

    public Project(int id, int managerId, DeveloperManager developerManager, ICollection<DeveloperTask> projectTasks, DateTime projectDate, string projectDescription, int contributionOrder)
    {
        Id = id;
        ManagerId = managerId;
        DeveloperManager = developerManager;
        ProjectTasks = projectTasks;
        ProjectDate = projectDate;
        ProjectDescription = projectDescription;
        ContributionOrder = contributionOrder;
    }
    public override string ToString()
    {
        return $"Project ID: {Id}\nManager ID: {ManagerId}\nProject Date: {ProjectDate}\n" +
               $"Project Description: {ProjectDescription}\nContribution Order: {ContributionOrder}";
    }

}