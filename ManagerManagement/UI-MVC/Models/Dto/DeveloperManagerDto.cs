using System.ComponentModel.DataAnnotations;

namespace ManagerManagement.UI.MVC.Models.Dto;

public class DeveloperManagerDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string Department { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]
    public double Salary { get; set; }
}