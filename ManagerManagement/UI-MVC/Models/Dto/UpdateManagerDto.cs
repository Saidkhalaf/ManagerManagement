using System.ComponentModel.DataAnnotations;
using ManagerManagement.BL.Domain;

namespace ManagerManagement.UI.MVC.Models.Dto;

public class UpdateManagerDto
{
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]
    public double Salary { get; set; }
}