using System.ComponentModel.DataAnnotations;
using ManagerManagement.BL.Domain;

namespace ManagerManagement.UI.MVC.Models.Dto;

public class NewProjectDto
{
    public int ManagerId { get; set; }
    public ICollection<DeveloperTask> Tasks { get; set; }
    public string ProjectDescription { get; set; }
    public DateTime HireDate { get; set; }
        
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]
    public double Salary { get; set; }
    
}