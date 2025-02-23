using System.ComponentModel.DataAnnotations;
using ManagerManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace ManagerManagement.UI.MVC.Models;

public class NewManagerModel
{
    //[RegularExpression(@"^\d+$",ErrorMessage = "Id is required.")]
    public int Id { get; set; }
        
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
        
    [Required(ErrorMessage = "Department is required.")]
    public string Department { get; set; }
    
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
        
    public ProjectStatus ProjectStatus { get; set; }
        
    [Display(Name = "Hire Date")]
    [Required(ErrorMessage = "The Hire Date field is required.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime HireDate { get; set; }
        
    [Display(Name = "Salary")]
    [Required(ErrorMessage = "The Salary field is required.")]
    [Range(0, 1000000, ErrorMessage = "The Salary must be between 0 and 1000000.")]
    public double Salary { get; set; }
}